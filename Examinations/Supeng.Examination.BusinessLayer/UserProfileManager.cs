using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IUserProfileManager : ICrud<UserProfile>, IDisposable
    {
        UserProfile Logon(string userCode, string password);

        string ChangePassword(string userCode, string password, string newPassword);

        string EditProfile(UserProfile profile);

        IList<UserSelectionViewModel> SelectTestUserList(int siteId = 0);

        TestUserSelectionViewModel SelectUserSelectViewModel(int testPaperId, int siteId = 0);

        string ResetPassword(int userId);
    }

    public class UserProfileManager : IUserProfileManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<UserProfile> SelectList()
        {
            return _context.UserProfiles.ToList();
        }

        public UserProfile SelectById(int id)
        {
            return _context.UserProfiles.FirstOrDefault(f => f.UserId == id);
        }

        public string Create(UserProfile data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.Password))
                {
                    data.Password = "123";
                }
                data.EncryptPassword();
                _context.UserProfiles.Add(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(UserProfile profile)
        {
            try
            {
                _context.Entry(profile).State = EntityState.Modified;
                _context.Entry(profile).Property(p => p.Password).IsModified = false;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(int id)
        {
            try
            {
                var data = SelectById(id);
                if (data == null)
                {
                    throw new KeyNotFoundException("Not found");
                }
                _context.UserProfiles.Remove(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public UserProfile Logon(string userCode, string password)
        {
            var list = _context.UserProfiles.Where(f => f.UserCode.Equals(userCode)).ToList();
            var userProfile = list.FirstOrDefault(f => f.ComparePassword(password));
            if (userProfile == null)
            {
                return null;
            }
            if (userProfile.SecurityRoleId != null)
            {
                userProfile.Roles =
                    _context.SecurityRoleActions
                        .Where(w => w.SecurityRoleId == userProfile.SecurityRoleId.Value)
                        .Select(s => s.Action.Name).ToArray();
            }
            else
            {
                userProfile.Roles = new string[0];
            }
            return userProfile;
        }

        public string ChangePassword(string userCode, string password, string newPassword)
        {
            try
            {
                var user = Logon(userCode, password);
                if (user == null)
                {
                    return "旧密码错误，登录失败.";
                }
                user.Password = newPassword;
                user.EncryptPassword();
                _context.Entry(user).Property(p => p.Password).IsModified = true;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EditProfile(UserProfile profile)
        {
            try
            {
                var user = SelectById(profile.UserId);
                _context.Entry(user).Property(p => p.Name).IsModified = true;
                _context.Entry(user).Property(p => p.Gender).IsModified = true;
                _context.Entry(user).Property(p => p.Mobile).IsModified = true;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message + "UserID:" + profile.UserId;
            }
        }

        public IList<UserSelectionViewModel> SelectTestUserList(int siteId = 0)
        {
            var list = new List<UserSelectionViewModel>();
            var users = siteId == 0 ? _context.UserProfiles : _context.UserProfiles.Where(w => w.SiteId == siteId);
            foreach (var user in users)
            {
                list.Add(new UserSelectionViewModel
                {
                    UserId = user.UserId,
                    UserCode = user.UserCode,
                    UserName = user.Name,
                    SiteName = user.Site == null ? "" : user.Site.Name
                });
            }
            return list;
        }

        public TestUserSelectionViewModel SelectUserSelectViewModel(int testPaperId, int siteId = 0)
        {
            var vm = new TestUserSelectionViewModel
            {
                TestId = testPaperId,
                SiteId = siteId,
                SelectedItems = (from ut in _context.UserTests.Where(w => w.TestId == testPaperId)
                                 join u in siteId == 0 ? _context.UserProfiles : _context.UserProfiles.Where(w => w.SiteId == siteId)
                                     on ut.UserId equals u.UserId
                                 select ut.UserId).ToList()
            };
            return vm;
        }

        public string ResetPassword(int userId)
        {
            var user = _context.UserProfiles.FirstOrDefault(f => f.UserId == userId);
            if (user == null)
            {
                return "Could not found the user";
            }
            try
            {
                user.Password = "123";
                user.EncryptPassword();
                _context.Entry(user).Property(p => p.Password).IsModified = true;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}