﻿namespace ClassWork.Models
{
    public class AuthResponse
    {
        public AuthResponse() : this(string.Empty, string.Empty)
        {
        }

        public AuthResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
        }

        public string AccessToken { get; }

        public string RefreshToken { get; }
    }
}
