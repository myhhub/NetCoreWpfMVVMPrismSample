using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismMetroSample.Infrastructure.Constants
{
  public static  class Global
    {
        private static List<User> _allUsers;
        public static List<User> AllUsers
        {
            get { return _allUsers; }
            set { _allUsers = value; }
        }
    }
}
