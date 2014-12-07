using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures
{
    public interface IFixture
    {
        string Name { get; }
        void Fade(double intensity, double milliseconds);
    }
}
