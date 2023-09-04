using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.ChatApp.GoogleLogin
{
    public class ExternalAuthDto
    {
        public const string PROVIDER = "google";

        [Required]
        public string IdToken { get; set; }

    }
}
