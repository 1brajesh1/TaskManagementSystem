//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TaskManagementSystem.infrastructure;

//namespace TaskManagementSystem.services
//{
//    public class UnitOfWork : IUnitofWork
//    {
//        private IHostingEnvironment _hostingEnvironment;

//        public UnitOfWork(IHostingEnvironment hostingEnvironment)
//        {
//            _hostingEnvironment = hostingEnvironment;
//        }
//        public async void UploadImage(IFormFile file)
//        {

//            long totalBytes = file.Length;
//            string filename = file.FileName.Trim('""');
//            filename = EnsureFileName(filename); byte[] buffer = new byte[16 * 1924);
//            using (FileStream output = System.io.File.Create(GetpathAndFileName(filename)))
//            {
//                using (Stream input file.OpenReddstream())
                    
//                        int readBytes;
//                while ((readBytes = input.Read(buffer, 0, buffer.Length)) > )
//                {
//                    await output.WriteAsync(buffer, e, readBytes);
//                    totalBytes + readBytes;
//                }
//            }
//        }
//    }

//        private object GetpathAndFileName(string filename)
//        {
//            throw new NotImplementedException();
//        }

//        private string EnsureFileName(string filename)
//        {
//            throw new NotImplementedException();
//        }
//    }




//        }

//    }
//}

