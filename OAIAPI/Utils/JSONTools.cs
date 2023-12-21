using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Promezio.OAIAPI.Utils;
public class JSONTools {
    public static bool IsValidJSON(string strInput) {
        if (string.IsNullOrWhiteSpace(strInput)) { return false; }

        try {
            JsonDocument.Parse(strInput);
            return true;
        } catch (JsonException) {
            return false;
        }
    }
}

