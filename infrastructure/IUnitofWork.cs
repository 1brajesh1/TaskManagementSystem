using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.infrastructure
{
   public interface IUnitofWork
    {
        void UploadImage(IFormFile file);
    }
}
