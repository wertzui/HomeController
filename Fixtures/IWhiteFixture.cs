using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures
{
    public interface IWhiteFixture : IFixture
    {
        [Range(0, 1)]
        double Intensity { get; set; }
    }
}
