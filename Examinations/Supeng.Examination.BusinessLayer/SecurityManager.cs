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

    #region Security Action

    public interface ISecurityActionManager : ICrud<SecurityAction>, IDisposable
    {
    }

    public class SecurityActionManager : ISecurityActionManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<SecurityAction> SelectList()
        {
            return _context.SecurityActions.ToList();
        }

        public SecurityAction SelectById(int id)
        {
            return _context.SecurityActions.FirstOrDefault(f => f.SecurityActionId == id);
        }

        public string Create(SecurityAction data)
        {
            try
            {
                _context.SecurityActions.Add(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(SecurityAction data)
        {
            try
            {
                var item = SelectById(data.SecurityActionId);
                _context.Entry(item).State = EntityState.Modified;
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
                var item = SelectById(id);
                _context.SecurityActions.Remove(item);
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

    #endregion

    public interface ISecurityRoleManager : ICrud<SecurityRole>, IDisposable
    {
        SecurityRoleActionViewModel SelectSecurityRoleActionViewModel(int roleId);
        string UpdateSecurity(int roleId, IList<int> actions);
    }

    public class SecurityRoleManager : ISecurityRoleManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        #region Cruds
        public IList<SecurityRole> SelectList()
        {
            return _context.SecurityRoles.ToList();
        }

        public SecurityRole SelectById(int id)
        {
            return _context.SecurityRoles.FirstOrDefault(f => f.SecurityRoleId == id);
        }

        public string Create(SecurityRole data)
        {
            try
            {
                _context.SecurityRoles.Add(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(SecurityRole data)
        {
            try
            {
                _context.Entry(data).State = EntityState.Modified;
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
                var item = SelectById(id);
                _context.SecurityRoles.Remove(item);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        public void Dispose()
        {
            _context.Dispose();
        }

        public SecurityRoleActionViewModel SelectSecurityRoleActionViewModel(int roleId)
        {

            var roleAction = new SecurityRoleActionViewModel
            {
                RoleId = roleId,
                ExistsActionIds = _context.SecurityRoleActions.Where(w => w.SecurityRoleId == roleId)
                    .Select(s => s.SecurityActionId)
                    .ToList(),
                AllActions = _context.SecurityActions.ToList()
            };
            return roleAction;
        }

        //todo: to be test
        public string UpdateSecurity(int roleId, IList<int> actions)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("SecurityRoleActonUpdate @p0,@p1",
                    roleId, string.Join(",", actions));
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}