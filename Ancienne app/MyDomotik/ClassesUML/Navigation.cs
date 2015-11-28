using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDomotik
{
    class Navigation
    {
        // champs
        private Vue pageFils;       // Vue accessible lors du clic sur l'icône dont dépend la Navigation courante

        public Vue PageFils
        {
            get { return pageFils; }
            set { pageFils = value; }
        }

        // constructeur
        public Navigation(Vue pageFils) 
        {
            this.PageFils = pageFils;
        }
    }
}
