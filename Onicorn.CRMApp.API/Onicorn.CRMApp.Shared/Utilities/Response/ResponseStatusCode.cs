using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.Shared.Utilities.Response
{
    public static class ResponseStatusCode
    {
        public static int OK = 200;
        public static int CREATED = 201;
        public static int NOT_FOUND = 404;
        public static int BAD_REQUEST = 400;
        public static int INTERNAL_SERVER_ERROR = 500;
        public static int NO_CONTENT = 204;
        public static int UNAUTHORİZED = 401;
        public static int FORBİDDEN = 403;
    }
}
