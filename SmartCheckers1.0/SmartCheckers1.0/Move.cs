using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SmartCheckers1._0
{
    class Move
    {        
        public Vector2 from;
        public Vector2 to;

        public Move(Vector2 from, Vector2 to)
        {            
            this.from = from;
            this.to = to;
        }
    }
}
