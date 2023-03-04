using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

public class XmlConfig
{
    public static int getOrderId()
    {
        XElement config = XMLTools.LoadListFromXMLElement(@"Config.xml");
        int id = (int)config.Element("idOrder")!;
        id++;
        config.Element("idOrder")!.SetValue(id);
        XMLTools.SaveListToXMLElement(config, @"Config.xml");
        // config.Save(@"Config.xml");
        return id;

    }
    public static int getOrderItemId()
    {
        XElement config = XMLTools.LoadListFromXMLElement(@"Config.xml");
        int id = (int)config.Element("idOrderItem")!;
        id++;
        config.Element("idOrderItem")!.SetValue(id);
        XMLTools.SaveListToXMLElement(config, @"Config.xml");
        return id;

    }
    public static int getProductId()
    {
        XElement config = XMLTools.LoadListFromXMLElement(@"Config.xml");
        int id = (int)config.Element("idProduct")!;
        id++;
        config.Element("idProduct")!.SetValue(id);
        XMLTools.SaveListToXMLElement(config, @"Config.xml");
        return id;

    }
}
