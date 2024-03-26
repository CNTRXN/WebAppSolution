using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models.UserData
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
