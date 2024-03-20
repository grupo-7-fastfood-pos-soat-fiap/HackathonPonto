using HackathonPonto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HackathonPonto.Application.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; } = "";

        public TokenViewModel()
        {

        }

        public TokenViewModel(string token)
        {
            Token=token;
        }
    }
}
