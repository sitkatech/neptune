using System.Diagnostics;
using CommandLine;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.DataMovement;

namespace AzureFileRestore;

internal class Options
{

    [Option('m', "Method", Required = true, HelpText = "What copy method to use.")]
    public CopyMethod Method { get; set; }
    
    [Option('s', "Source", Required = true, HelpText = "What source to use.")]
    public string Source { get; set; }

    [Option('d', "Destination", Required = true, HelpText = "What destination to use.")]
    public string Destination { get; set; }

    [Option('c', "Containers", Required = true, HelpText = "What containers to copy.")]
    public IEnumerable<string> Containers { get; set; }
}

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        var result = 1;
        await Parser.Default.ParseArguments<Options>(args).WithParsedAsync(async options =>
        {
            result = await CopyFiles(options.Method, options.Source, options.Destination, options.Containers);
        });

        return result;
    }

    private static async Task<int> CopyFiles(CopyMethod copyMethod, string sourceConnectionString, string destinationConnectionString, IEnumerable<string> containers)
    {
        if (destinationConnectionString.Contains("prod"))
        {
            Console.WriteLine("Destination connection string cannot contain 'prod' to ensure that production data is not modified.");
            return 1;
        }

        var sourceAccount = CloudStorageAccount.Parse(sourceConnectionString);
        var destinationAccount = CloudStorageAccount.Parse(destinationConnectionString);

        var copyDirectoryOptions = new CopyDirectoryOptions()
        {
            Recursive = true,
            PreserveSMBAttributes = true,
        };

        TransferCheckpoint checkpoint = null!;
        var transferContext = GetDirectoryTransferContext(checkpoint);

        var stopwatch = Stopwatch.StartNew();
        foreach (var container in containers)
        {
            var sourceContainerDirectory = await GetBlobDirectory(sourceAccount, container);
            var destinationContainerDirectory = await GetBlobDirectory(destinationAccount, container);
            try
            {
                await TransferManager.CopyDirectoryAsync(sourceContainerDirectory, destinationContainerDirectory, copyMethod, copyDirectoryOptions, transferContext);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"There was an error restoring files for the container {container}.");
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
        stopwatch.Stop();

        Console.WriteLine($"Finished restoring files in {stopwatch.Elapsed:hh\\:mm\\:ss}.");
        return 0;
    }

    private static async Task<CloudBlobDirectory> GetBlobDirectory(CloudStorageAccount account, string containerName)
    {
        var blobClient = account.CreateCloudBlobClient();
        
        var container = blobClient.GetContainerReference(containerName);
        await container.CreateIfNotExistsAsync();

        var blobDirectory = container.GetDirectoryReference("");
        return blobDirectory;
    }

    private static DirectoryTransferContext GetDirectoryTransferContext(TransferCheckpoint checkpoint)
    {
        var context = new DirectoryTransferContext(checkpoint)
        {
            ProgressHandler = new Progress<TransferStatus>(progress =>
            {
                var statusText = $"Bytes transferred: {FormatBytes(progress.BytesTransferred)}";
                Console.WriteLine(statusText);
            })
        };

        return context;
    }

    private static string FormatBytes(long bytes)
    {
        const int scale = 1024;
        string[] units = { "B", "KB", "MB", "GB", "TB" };
        var unitIndex = 0;

        var scaledValue = (double) bytes;
        while (scaledValue >= scale && unitIndex < units.Length - 1)
        {
            scaledValue /= scale;
            unitIndex++;
        }

        return $"{scaledValue:F2} {units[unitIndex]}";
    }
}