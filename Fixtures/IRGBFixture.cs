using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures
{
    public interface IRGBFixture
    {
        [Range(0, 1)]
        double Red { get; set; }
        [Range(0, 1)]
        double Green { get; set; }
        [Range(0, 1)]
        double Blue { get; set; }
        Color Color { get; set; }

        void Fade(double red, double green, double blue, double milliseconds);
        void Fade(Color color, double milliseconds);
    }
}
