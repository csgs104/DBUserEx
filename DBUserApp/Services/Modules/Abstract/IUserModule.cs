namespace DBUserApp.Services.Modules.Abstract;

public interface IUserModule : IModule
{
    public void InsertUser();
    public void UpdateUser();
    public void DeleteUser();
    public void SearchUserByEmail();
    public void WritingUserData();
}