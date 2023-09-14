using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Context;
using Azin.Shadi.DAL.Entities.User;
using Azin.Shadi.Core.Generators;
using Azin.Shadi.Core.Convertors;
using Azin.Shadi.Core.Security;
using Azin.Shadi.DAL.Entities.Transaction;
using Microsoft.EntityFrameworkCore;
using Azin.Shadi.Core.Tools;

namespace Azin.Shadi.Core.Services;

public class UserService : IUserService
{
    private readonly AzinShadiContext _context;

    public UserService(AzinShadiContext context)
    {
        _context = context;
    }

    public int AddUser(UserRegisterViewModel user)
    {
        User newUser = new User
        {
            Username = Generator.CreateUniqueText().Substring(0, 10),
            Name = user.UserName,
            Email = TextFixed.EmailFix(user.Email),
            Password = PasswordHelper.EncodePasswordMd5(user.Password),
            RegisterDate = DateTime.Now,
            IsActive = true,
            ActivationCode = Generator.CreateUniqueText(),
            ProfileName = "default-profile.jpg"
        };
        _context.Users.Add(newUser);

        _context.SaveChanges();
        return newUser.UserId;
    }

    public bool ChangeUserPassword(string username, string newpassword)
    {
        var user = GetUserByUsername(username);
        user.Password = PasswordHelper.EncodePasswordMd5(newpassword);
        return UpdateUser(user);
    }

    public bool EditUserProfile(string username, EditProfileViewModel profile)
    {
        if (profile.ProfileImage != null && profile.ProfileImage.IsImage())
        {
            string imagePath = "";
            if (profile.ProfileName != "default-profile.jpg")
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\UserProfile", profile.ProfileName);
                FileTools.DeleteFile(imagePath);

                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\UserProfile\\Thumb", profile.ProfileName);
                FileTools.DeleteFile(imagePath);
            }

            profile.ProfileName = Generator.CreateUniqueText() + Path.GetExtension(profile.ProfileImage.FileName);
            FileTools.SaveImage(profile.ProfileImage, profile.ProfileName, "UserProfile", true);
        }
        var user = GetUserByUsername(username);

        user.Name = profile.Name;
        user.ProfileName = profile.ProfileName;
        user.Email = profile.Email;

        return UpdateUser(user);
    }

    public SideBarUserPanelViewModel GetSidebarUserData(string username)
    {
        var user = _context.Users.SingleOrDefault(t => t.Username == username);
        return new SideBarUserPanelViewModel
        {
            Name = user.Name,
            ProfileName = user.ProfileName,
            RegisterDate = user.RegisterDate,
            Email = user.Email
        };
    }

    public User GetUserByActivationCode(string activationCode)
    {
        return _context.Users.SingleOrDefault(t => t.ActivationCode == activationCode);

    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.SingleOrDefault(t => t.Email == email);
    }

    public User GetUserById(int id)
    {
        return _context.Users.SingleOrDefault(t => t.UserId == id);
    }

    public User GetUserByUsername(string username)
    {
        return _context.Users.Single(t => t.Username == username);
    }

    public EditProfileViewModel GetUserDataForEditProfile(User user)
    {
        return new EditProfileViewModel
        {
            Name = user.Name,
            Email = user.Email,
            ProfileName = user.ProfileName
        };
    }

    public int GetWalletBalance(string username)
    {
        int userId = GetUserIdByUsername(username);

        var deposit = _context.Transactions.Where(t => t.TransactionTypeId == 3 && t.IsComplete == true).ToList();
        var harvest = _context.Transactions.Where(t => t.TransactionTypeId == 2 && t.IsComplete == true).ToList();

        return deposit.Sum(t => t.Amount) - harvest.Sum(t => t.Amount);
    }

    public int GetUserIdByUsername(string username)
    {
        return _context.Users.Single(t => t.Username == username).UserId;
    }

    public bool IsEmailExist(string email)
    {
        if (_context.Users.Any(u => u.Email == email)) return true;
        else return false;
    }

    public User LoginUser(LoginViewModel user)
    {
        string email = TextFixed.EmailFix(user.Email);
        string password = PasswordHelper.EncodePasswordMd5(user.Password);
        return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
    }

    public bool UpdateUser(User user)
    {
        _context.Update(user);
        var result = _context.SaveChanges();
        return result == 1;
    }

    public bool UserExistValidation(string username, string password)
    {
        string hasspassword = PasswordHelper.EncodePasswordMd5(password);
        return _context.Users.Any(user => user.Username == username && user.Password == hasspassword);
    }
    public bool ValidateEmail(string activationCode)
    {
        var user = _context.Users.FirstOrDefault(t => t.ActivationCode == activationCode);
        if (user == null || user.EmailConfrimation == true) return false;
        user.EmailConfrimation = true;
        user.ActivationCode = Generator.CreateUniqueText();
        _context.SaveChanges();
        return true;
    }

    public List<TransactionViewModel> GetUserWalletTransaction(string username)
    {
        int userId = GetUserIdByUsername(username);
        return _context.Transactions.Where(t => t.UserId == userId && t.IsComplete == true).Where(t => t.TransactionTypeId == 2 || t.TransactionTypeId == 3).Select(t => new TransactionViewModel
        {
            Amount = t.Amount,
            Description = t.Description,
            PayDate = t.PayDate,
            TransactionTypeId = t.TransactionTypeId
        }).ToList();

    }

    public int AddTransaction(string username, int amount, string description, int TransactionType)
    {
        Transaction transaction = new Transaction
        {
            Amount = amount,
            Description = description,
            PayDate = DateTime.Now,
            TransactionTypeId = TransactionType,
            UserId = GetUserIdByUsername(username),
            IsComplete = false
        };
        _context.Add(transaction);
        _context.SaveChanges();
        return transaction.TransactionId;

    }

    public Transaction GetTransactionById(int id)
    {
        return _context.Transactions.Find(id);
    }

    public void UpdateTransaction(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        _context.SaveChanges();
    }

    public ShowUserForAdminViewModel GetUsers(int pageId = 1, string emailFilter = "", string nameFilter = "")
    {
        IQueryable<User> filteredUsers = _context.Users;

        if (!string.IsNullOrEmpty(emailFilter))
        {
            filteredUsers = filteredUsers.Where(u => u.Email.Contains(emailFilter));
        }
        if (!string.IsNullOrEmpty(nameFilter))
        {
            filteredUsers = filteredUsers.Where(u => u.Name.Contains(nameFilter));
        }


        int take = 20;
        int skip = (pageId - 1) * take;

        return new ShowUserForAdminViewModel
        {
            Users = filteredUsers.OrderBy(t => t.Name).Skip(skip).Take(take).ToList(),
            CurrentPage = pageId,
            PageCount = filteredUsers.Count() / take
        };
    }

    public int AddUserForAdminPanel(CreateUserViewModel User)
    {
        string ImageName = "default-profile.jpg";
        if (User.UserAvatar != null && User.UserAvatar.IsImage())
        {
            ImageName = Generator.CreateUniqueText() + Path.GetExtension(User.UserAvatar.FileName);
            FileTools.SaveImage(User.UserAvatar, ImageName, "UserProfile", true);
        }
        User newUser = new DAL.Entities.User.User
        {
            Name = User.Name,
            ActivationCode = Generator.CreateUniqueText(),
            Email = User.Email,
            IsActive = true,
            EmailConfrimation = true,
            Password = PasswordHelper.EncodePasswordMd5(User.Password),
            Username = Generator.CreateUniqueText().Substring(0, 10),
            ProfileName = ImageName,
            RegisterDate = DateTime.Now
        };


        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser.UserId;
    }

    public EditUserViewModel GetInformationFoEditPanel(int userId)
    {
        User user = _context.Users.Find(userId) ?? new User();
        EditUserViewModel editedUser = new EditUserViewModel
        {
            UserId = user.UserId,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive,
            ProfileName = user.ProfileName,
            RegisterDate = user.RegisterDate
        };
        editedUser.Roles = _context.UserRoles.Where(t => t.UserId == editedUser.UserId).Select(t => t.RoleId).ToList();
        return editedUser;
    }

    public bool EditUserByAdmin(EditUserViewModel EditUser, List<int> updateRoles)
    {
        string username = GetUserById(EditUser.UserId).Username;
        if (!string.IsNullOrEmpty(EditUser.Password))
        {
            ChangeUserPassword(username, EditUser.Password);
        }
        if (EditUser.ProfilePicture != null && EditUser.ProfilePicture.IsImage())
        {
            string path = "";
            if (EditUser.ProfileName != "default-profile.jpg")
            {
                //delete Past Profile
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\UserProfile", EditUser.ProfileName);
                FileTools.DeleteFile(path);

                //delete Past ThumbProfile
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\UserProfile\\Thumb", EditUser.ProfileName);
                FileTools.DeleteFile(path);
            }

            EditUser.ProfileName = Generator.CreateUniqueText() + Path.GetExtension(EditUser.ProfilePicture.FileName);
            FileTools.SaveImage(EditUser.ProfilePicture, EditUser.ProfileName, "UserProfile", true);
        }


        var user = GetUserByUsername(username);
        user.Email = EditUser.Email;
        user.ProfileName = EditUser.ProfileName;
        user.Name = EditUser.Name;
        return UpdateUser(user);
    }

    public bool DeleteUser(int id)
    {
        User user = GetUserById(id);
        user.IsDelete = true;
        return UpdateUser(user);
    }

    public ShowUserForAdminViewModel GetDeletedUsers(int pageId = 1, string emailFilter = "", string nameFilter = "")
    {
        IQueryable<User> filteredUsers = _context.Users.IgnoreQueryFilters().Where(t => t.IsDelete);

        if (!string.IsNullOrEmpty(emailFilter))
        {
            filteredUsers = filteredUsers.Where(u => u.Email.Contains(emailFilter));
        }
        if (!string.IsNullOrEmpty(nameFilter))
        {
            filteredUsers = filteredUsers.Where(u => u.Name.Contains(nameFilter));
        }


        int take = 20;
        int skip = (pageId - 1) * take;

        return new ShowUserForAdminViewModel
        {
            Users = filteredUsers.OrderBy(t => t.Name).Skip(skip).Take(take).ToList(),
            CurrentPage = pageId,
            PageCount = filteredUsers.Count() / take
        };
    }

    public List<Transaction> GetAllTransactions()
    {
        return _context.Transactions.Include(t=>t.TransactionType).Include(t=>t.Related_User).ToList();
    }
}
