namespace DWShop.Shared.Wrapper
{
    public interface IResult
    {
        List<string> Messages { get; set; }
        bool Succeded { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
        
    }

    

    //https://gist.github.com/ychavez/9603a127e599e1f0038bcf224e05412a

}
