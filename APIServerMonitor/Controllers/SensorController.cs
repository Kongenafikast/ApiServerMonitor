using APIServerMonitor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;

namespace APIServerMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorGetController : Controller
    {
        MySqlDb mySqlDb;
        public SensorGetController()
        {
            mySqlDb = new MySqlDb();
        }

        [HttpGet("GetByGroupId")]
        public async Task<IActionResult> GetByGroupId(int groupId)
        {
            string qurey = $"SELECT * FROM Sensors WHERE groupid = {groupId}";
            var result = await mySqlDb.GetData(qurey);
            return Ok(result);
        }


        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetByDate(DateTime created)
        {
            string qurey = $"SELECT * FROM Sensors WHERE created >= {created}";
            var result = await mySqlDb.GetData(qurey);
            return Ok(result);
        }

        [HttpGet("GetByGroupIdAndTypeId")]
        public async Task<IActionResult> GetByGroupIdAndTypeId(int groupId, int typeId)
        {
            string qurey = $"SELECT * FROM Sensors WHERE groupid = {groupId} AND typeId = {typeId}";
            var result = await mySqlDb.GetData(qurey);
            return Ok(result);
        }

        [HttpGet("GetByGroupIdTypeIdAndDate")]
        public async Task<IActionResult> GetByGroupIdTypeIdAndDate(int groupId, int typeId, DateTime created)
        {
            string qurey = $"SELECT * FROM Sensors WHERE groupid = {groupId} AND typeId = {typeId} AND created >= {created}";
            var result = await mySqlDb.GetData(qurey);
            return Ok(result);
        }

        [HttpGet("GetByGroupIdAndDate")]
        public async Task<IActionResult> GetByGroupIdAndDate(int groupId, DateTime created)
        {
            string qurey = $"SELECT * FROM Sensors WHERE groupid = {groupId} AND created >= {created}";
            var result = await mySqlDb.GetData(qurey);
            return Ok(result);
        }

        [HttpGet("GetByTypeIdAndDate")]
        public async Task<IActionResult> GetByTypeIdAndDate(int typeId, DateTime created)
        {
            string qurey = $"SELECT * FROM Sensors WHERE groupid = {typeId} AND created >= {created}";
            var result = await mySqlDb.GetData(qurey);
            return Ok(result);
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class SensorPostController : Controller
    {
        MySqlDb mySqlDb;
        public SensorPostController()
        {
            mySqlDb = new MySqlDb();
        }

        [HttpPost("PostSensors")]
        public async Task<IActionResult> PostSensors(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                bool result = true;
                json = json.Replace(@"\", string.Empty);
                JObject data = (JObject)JsonConvert.DeserializeObject(json);
                List<string> querys = new();
                querys.Add($"INSERT INTO Sensors (typeId, dataValue, groupid ) VALUES (1,{data["Co2"]},{data["GroupId"]})");
                querys.Add($"INSERT INTO Sensors (typeId, dataValue, groupid ) VALUES (2,{data["TVOC"]},{data["GroupId"]})");
                querys.Add($"INSERT INTO Sensors (typeId, dataValue, groupid ) VALUES (3,{data["Temprature"]},{data["GroupId"]})");
                querys.Add($"INSERT INTO Sensors (typeId, dataValue, groupid ) VALUES (4,{data["Humitity"]},{data["GroupId"]})");


                foreach (string query in querys)
                {
                   result = await mySqlDb.PostData(query);
                }
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("something went wrong");
                }

            }
            return BadRequest("didn't have the right parameters");

        }
    }
}
