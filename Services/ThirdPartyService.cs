using IAR.Data;
using IAR.Models;

namespace IAR.Services
{
    public class ThirdPartyService
    {
        private readonly RegisterContext _context = default!;

        public ThirdPartyService(RegisterContext context)
        {
            _context = context;
        }

        public IList<ThirdParty> GetThirdParties()
        {
            if(_context.ThirdParties != null)
            {
                return _context.ThirdParties.ToList();
            }
            return new List<ThirdParty>();
        }

        public void AddThirdParty(ThirdParty thirdParty)
        {
            if (_context.ThirdParties != null)
            {
                _context.ThirdParties.Add(thirdParty);
                _context.SaveChanges();
            }
        }

        public void DeleteThirdParty(int id)
        {
            if (_context.ThirdParties != null)
            {
                var thirdParty = _context.ThirdParties.Find(id);
                if (thirdParty != null)
                {
                    _context.ThirdParties.Remove(thirdParty);
                    _context.SaveChanges();
                }
            }            
        }
    }
}