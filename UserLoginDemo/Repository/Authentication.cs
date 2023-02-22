using Microsoft.EntityFrameworkCore;
using UserLoginDemo.Models;
using UserLoginDemo.Data;
using System;
using Microsoft.AspNetCore.Identity;

namespace UserLoginDemo.Repository
{
    public class Authentication : IUserLogin
    {
        private readonly LoginDbcontext _context;

        public Authentication(LoginDbcontext context)
        {
            _context = context;
        }
        #region "Authentication"
        public async Task<String> AuthenticateUser(Models.UserLoginInfo userLoginInfo)
        {
            string results = "";
            UserInfo selectedUserInfo = await _context.UserInfo.FirstOrDefaultAsync(authUser => authUser.COR_USERID == userLoginInfo.COR_USERID);
            if(selectedUserInfo != null)
            {
                results = ValidateUserLogin(userLoginInfo, selectedUserInfo);
            }

            return results;
        }

        protected string ValidateUserLogin(Models.UserLoginInfo userLoginInfo, UserInfo selectedUserInfo)
        {
            string result = "";
            //check if not exist max retry or login interval > 60mins
            if (selectedUserInfo.LOGIN_STATUS == 1 || DateTime.Now.Subtract(selectedUserInfo.LOGIN_DTTM).TotalMinutes > 60) 
            {
                UserInfo dataToBeUpdated = selectedUserInfo;
                UserLog userLog = new UserLog();
                if (selectedUserInfo.PASSWORD == userLoginInfo.PASSWORD)
                {
                    dataToBeUpdated.LOGIN_DTTM = DateTime.Now;
                    dataToBeUpdated.PASSWORD_RETRY = 0;
                    dataToBeUpdated.LOGIN_STATUS = 1;
                    userLog.COR_USERID = userLoginInfo.COR_USERID;
                    userLog.ACCESS_TIME =  DateTime.Now;
                    userLog.ACTION = "Login Successfully";
                }
                else 
                { 
                    dataToBeUpdated.LOGIN_DTTM = dataToBeUpdated.PASSWORD_RETRY > 0? dataToBeUpdated.LOGIN_DTTM : DateTime.Now;
                    dataToBeUpdated.PASSWORD_RETRY = DateTime.Now.Subtract(selectedUserInfo.LOGIN_DTTM).TotalMinutes < 15 ? ++dataToBeUpdated.PASSWORD_RETRY : dataToBeUpdated.PASSWORD_RETRY;
                    dataToBeUpdated.LOGIN_STATUS = (short)(dataToBeUpdated.PASSWORD_RETRY >= 3 ? 0 : 1);
                    userLog.COR_USERID = userLoginInfo.COR_USERID;
                    userLog.ACCESS_TIME = DateTime.Now;
                    userLog.ACTION = "Login Failed";
                    result = "Authentication error. Please try again later";
                }
                _context.UserInfo.Update(dataToBeUpdated);
                _context.UserLog.Add(userLog);
                _context.SaveChanges();
            }
            else
            {
                result = "Your account being locked. Please try again after 1 hour.";
            }
            return result;
        }

        #endregion "Authentication"


        #region "GetUser"
        public List<UserInfo> GetUser()
        {
            List<UserInfo> allUserInfo = _context.UserInfo.Select(x => x).ToList();
            return allUserInfo;
        }
        #endregion "GetUser"


        #region "Logout"
        public void Logout(string userId)
        {
            UserLog userLog = new UserLog();
            UserInfo dataToBeUpdated = _context.UserInfo.FirstOrDefault(authUser => authUser.COR_USERID == userId);
            if(dataToBeUpdated != null)
            {
                dataToBeUpdated.LOGOUT_DTTM = DateTime.Now;
                userLog.COR_USERID = userId;
                userLog.ACCESS_TIME = DateTime.Now;
                userLog.ACTION = "Logout Successfully";
                _context.UserInfo.Update(dataToBeUpdated);
                _context.UserLog.Add(userLog);
                _context.SaveChanges();
            }
        }
        #endregion "Logout"
    }
}
