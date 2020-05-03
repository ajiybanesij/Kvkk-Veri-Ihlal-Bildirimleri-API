using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Helper
{
    public class xPaths
    {
        public readonly string count = "//a[contains(@class, 'page-link')]";

        public readonly string big_ihlal_title = "/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[2]/h3";
        public readonly string big_ihlal_date = "/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[2]/p[1]";
        public readonly string big_ihlal_url = "/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[2]/div/a";
        public readonly string big_ihlal_img = "/html/body/div[3]/div/div[1]/div[1]/div[1]/div/div[1]/img";

        public readonly string items_list = "//div[contains(@class, 'col-lg-4 col-md-6 col-sm-12 pb-3')]";
        public readonly string items_title = "/html/body/div[3]/div/div[1]/div[1]/div[{0}]/div/div[2]/h4/a";
        public readonly string items_date = "/html/body/div[3]/div/div[1]/div[1]/div[{0}]/div/div[2]/p/span/a";
        public readonly string items_url = "/html/body/div[3]/div/div[1]/div[1]/div[{0}]/div/div[2]/h4/a";
        public readonly string items_image = "/html/body/div[3]/div/div[1]/div[1]/div[{0}]/div/div[1]/a/img";



    }
}
