using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Shared.Utilities.Response
{
    public class ResponseStatusCode
    {
        public const int OK = 200;
        public const int CREATED = 201;
        public const int NOT_FOUND = 404;
        public const int BAD_REQUEST = 400;
        public const int INTERNAL_SERVER_ERROR = 500;
        public const int NO_CONTENT = 204;
        public const int UNAUTHORİZED = 401;
        public const int FORBİDDEN = 403;
    }
}
