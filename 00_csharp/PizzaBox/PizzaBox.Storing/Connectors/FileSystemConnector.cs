using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Storing.Connectors
{
  public class FileSystemConnector
  {
    private const string _userPath = @"userStorage.xml";
    private const string _orderPath = @"orderStorage.xml";
    private const string _storePath = @"storeStorage.xml";

    public List<User> UserReadXml(string path = _userPath)
    {
      var xml = new XmlSerializer(typeof(List<User>));
      var reader = new StreamReader(path);

      return xml.Deserialize(reader) as List<User>;
    }

    public void UserWriteXml( List<User> data, string path = _userPath)
    {
      var xml = new XmlSerializer(typeof(List<User>));
      var writer = new StreamWriter(path);
      xml.Serialize(writer, data);
    }

    public List<Order> OrderReadXml(string path = _orderPath)
    {
      var xml = new XmlSerializer(typeof(List<Order>));
      var reader = new StreamReader(path);

      return xml.Deserialize(reader) as List<Order>;
    }

    public void OrderWriteXml( List<Order> data, string path = _orderPath)
    {
      var xml = new XmlSerializer(typeof(List<Order>));
      var writer = new StreamWriter(path);
      xml.Serialize(writer, data);
    }

   public List<Store> StoreReadXml(string path = _storePath)
    {
      var xml = new XmlSerializer(typeof(List<Store>));
      var reader = new StreamReader(path);

      return xml.Deserialize(reader) as List<Store>;
    }

    public void StoreWriteXml( List<Store> data, string path = _storePath)
    {
      var xml = new XmlSerializer(typeof(List<Store>));
      var writer = new StreamWriter(path);
      xml.Serialize(writer, data);
    }
  }
}