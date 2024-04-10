﻿using RPABLL.Interfaces;
using RPADAL.DAL;
using RPADAL.IDAL;
using RPADTO.AccountInfo;
using RPADTO.Login;
using RPADTO.RequestDTO;
using RPAUtility;

namespace RPABLL.BLL
{
    public class LoginBLL : ILoginBLL
    {
        public readonly ILoginDAL _loginDAL;
        public readonly IAccountInfoDAL _accountInfoDAL;

        public LoginBLL(ILoginDAL loginDAL, IAccountInfoDAL accountInfoDAL)
        {
            _loginDAL = loginDAL;
            _accountInfoDAL = accountInfoDAL;
        }

        public LoginResponseDTO ValidateLoginCredentials(LoginRequestDTO requestDTO) 
        { 
            LoginResponseDTO responseDTO = _loginDAL.GetUserCredentials(requestDTO);

            responseDTO.Authorized = true;
            if (requestDTO.UserName != responseDTO.Username)
            {
                responseDTO.Authorized = false;
                responseDTO.ValidationErrors.Add(Constants.IncorrectUsername);
            }

            if (requestDTO.Password != responseDTO.Password)
            {
                responseDTO.Authorized = false;
                responseDTO.ValidationErrors.Add(Constants.IncorrectPassword);
            }

            return responseDTO;
        }

        public void RegisterAccount(AccountRequestDTO requestDTO)
        {
            _accountInfoDAL.RegisterAccount(requestDTO);
        }
    }
}
