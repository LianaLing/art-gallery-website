using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryWebsite.Models
{
    // This class is to describe what an API Response should look like
    // All response returned from our API should be an instance of this class
    public class ApiResponse<T>
    {
        public T Data;
        public Error Error;

        public ApiResponse(T data)
        {
            this.Data = data;
        }
        public ApiResponse(T data, Error error)
        {
            this.Data = data;
            this.Error = error;
        }
    }
}
