using Microsoft.AspNetCore.Http;
using RentACar.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Abstract
{
    /// <summary>
    /// <see Storage service
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Upload a file
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        Task Upload(FileModel fileModel);

    }
}
