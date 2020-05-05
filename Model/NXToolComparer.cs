using System.Collections.Generic;

namespace ToolHolder_NS.Model
{
    class NXToolComparer : IComparer<thNXTool>
        {
            public int Compare(thNXTool x, thNXTool y)
            {               
                if (x.ToolNumber < y.ToolNumber)
                    return 1;
                if (x.ToolNumber > y.ToolNumber)
                    return -1;
                
                return 0;
            }
        }
    
}