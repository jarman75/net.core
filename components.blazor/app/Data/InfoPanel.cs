using System.Collections.Generic;

namespace app.Data
{
    public class InfoPanel
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public List<Section> Sections {get; set;}
    }
    public class Section {
        public string Tittle {get; set;}
        public List<Block> Blocks {get; set;}
        
    }

    public class Block {
        public int Id {get; set;}
        public int Order {get; set;}
        public List<Property> Props {get; set;}
    }

    public class Property {
        public string Label {get; set;}
        public object Value {get; set;}
        public int Order {get; set;}
    }
}