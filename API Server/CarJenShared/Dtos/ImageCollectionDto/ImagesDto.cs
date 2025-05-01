using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Dtos.ImageCollectionDto
{
    public class ImagesDto
    {
        public int? ImageCollectionID { get; set; }
        public int? CarID { get; set; }
        public string FrontView { get; set; }
        public string RearView { get; set; }
        public string SideView { get; set; }
        public string InteriorView { get; set; }
    }
}
