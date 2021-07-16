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
        public T data;
        public Error error;

        public ApiResponse(T data)
        {
            this.data = data;
        }
        public ApiResponse(T data, Error error)
        {
            this.data = data;
            this.error = error;
        }
    }
}
