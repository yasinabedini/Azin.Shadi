using Azin.Shadi.Core.DTOs;
using Azin.Shadi.DAL.Entities.Transaction;
using Azin.Shadi.DAL.Entities.User;

namespace Azin.Shadi.Core.Services.Interfaces;
public interface IUserService
{
    #region Account
    bool IsEmailExist(string email);
    int AddUser(UserRegisterViewModel user);
    User LoginUser(LoginViewModel user);
    bool ValidateEmail(string activationCode);
    User GetUserById(int id);
    int GetUserIdByUsername(string username);
    User GetUserByEmail(string email);
    User GetUserByUsername(string username);
    User GetUserByActivationCode(string activationCode);
    bool UpdateUser(User user);    
    #endregion

    #region UserPanel
    SideBarUserPanelViewModel GetSidebarUserData(string username);
    EditProfileViewModel GetUserDataForEditProfile(User user);              
    bool EditUserProfile(string username, EditProfileViewModel profile);
    bool UserExistValidation(string username, string password);
    bool ChangeUserPassword(string username, string newpassword);
    #endregion

    #region Transaction

    int GetWalletBalance(string username);
    List<TransactionViewModel> GetUserWalletTransaction(string username);
    int AddTransaction(string username, int amount, string description, int TransactionType);
    Transaction GetTransactionById(int id);
    void UpdateTransaction(Transaction transaction);
    List<Transaction> GetAllTransactions();
    #endregion

    #region Admin Panel
    ShowUserForAdminViewModel GetUsers(int pageId = 1, string emailFilter = "", string nameFilter = "");
    ShowUserForAdminViewModel GetDeletedUsers(int pageId = 1, string emailFilter = "", string nameFilter = "");
    int AddUserForAdminPanel(CreateUserViewModel User);
    EditUserViewModel GetInformationFoEditPanel(int userId);
    bool EditUserByAdmin(EditUserViewModel user,List<int> updateRoles);
    bool DeleteUser(int id);
    #endregion
}