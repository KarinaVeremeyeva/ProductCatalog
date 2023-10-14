﻿using ProductCatalog.BLL.Models;

namespace ProductСatalog.BLL.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
