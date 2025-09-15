
using Beauty_Salon.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_Salon
{
    public class DatabaseHelper
    {
        private static Beauty_Salon_Entities _context;

        public static Beauty_Salon_Entities GetContext()
        {
            if (_context == null)
                _context = new Beauty_Salon_Entities();
            return _context;
        }
    }
}
   
