
"Download Linter Docker Image"
docker pull github/super-linter:latest

"Run Linter"
docker run -e RUN_LOCAL=true -v c:\git\neptune:/tmp/lint github/super-linter

#docker run -e RUN_LOCAL=true -e VALIDATE_POWERSHELL=true -v c:\git\neptune:/tmp/lint github/super-linter


