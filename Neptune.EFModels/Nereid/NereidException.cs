namespace Neptune.EFModels.Nereid;

public class NereidException<TReq, TResp> : Exception
{
    public NereidException()
    { }

    public NereidException(string s) : base(s)
    {
    }

    public NereidException(string s, Exception exception) : base(s, exception)
    {
    }

    public TReq Request { get; set; }
    public TResp Response { get; set; }
}