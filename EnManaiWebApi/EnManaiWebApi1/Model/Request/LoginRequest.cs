﻿namespace EnManaiWebApi.Model.Request
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool ForgetPassword { get; set; }
    }
}
