using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Other
{
    public class EqualEntity
    {
        public static bool CabinetIsExist(List<Cabinet> cabinets, CabinetDTO eqCab) 
        {
            var eq = cabinets
                .Where(c => c.Num == eqCab.Num &&
                    c.PlanNum == eqCab.PlanNum &&
                    c.Width == eqCab.Width &&
                    c.Length == eqCab.Length &&
                    c.Height == eqCab.Height &&
                    c.Floor == eqCab.Floor
                ).FirstOrDefault();

            if (eq != null)
                return true;

            return false;
        }
    }
}
