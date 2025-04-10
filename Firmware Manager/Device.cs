using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using Firmware_Manager;

namespace AxisFirmwareUpgradeApp
{
    public class Device
    {
        public string SiteName { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Status { get; set; }

        public string Firmware { get; set; }
        public string TargetFirmware { get; set; }
        public string VmdStatus { get; set; }
        public string ObjectAnalyticsStatus { get; set; }
        public string Uptime { get; set; }

        public Device(string sname, string dname, string host, string port, string user, string pass)
        {
            SiteName = sname;
            DeviceName = dname;
            Host = host;
            Port = port;
            Username = user;
            Password = pass;
        }

        public string About(bool includeCreds = false)
        {
            string about = $"{SiteName}: {DeviceName} ({Host}:{Port})";
            if (includeCreds) { about += $" login: {Username}, {Password}"; }
            return about;
        }

        public string AboutCSV(bool includeCreds = false)
        {
            if (includeCreds)
            {
                return $"{SiteName},{DeviceName},{Host},{Port},{Username},{Password},{Status}";
            }
            else
            {
                return $"{SiteName},{DeviceName},{Host},{Port},{Status}";
            }
        }

        public string GetConnectionInfo()
        {
            return $"http://{Host}:{Port}/";
        }

        public string GetApplicationToggle(bool state, string app, bool includeURL = true)
        {
            string _state = state == true ? "start" : "stop";
            if (includeURL)
            {
                return $"{GetConnectionInfo()}axis-cgi/applications/control.cgi?action={_state}&package={app}";
            }
            else
            {
                return $"axis-cgi/applications/control.cgi?action={_state}&package={app}";
            }
        }

        public async Task<bool> Poke(TimeSpan timeout = default)
        {
            if (timeout == default) { timeout = TimeSpan.FromSeconds(5); }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, GetConnectionInfo());
            using CancellationTokenSource cts = new CancellationTokenSource(timeout);
            try
            {
                // Send the HEAD request
                HttpResponseMessage response = await Program.client.SendAsync(request, cts.Token);

                // Check if the status code indicates success
                return response.IsSuccessStatusCode;
            }
            catch (TaskCanceledException)
            {
                // TaskCanceledException may indicate a timeout
                Console.WriteLine("Request timed out.");
                return false;
            }
            catch (HttpRequestException)
            {
                // Handle exceptions if the URL is unreachable
                Console.WriteLine("Request timed out.");
                return false;
            }
        }
        /*
        public async Task<string> SetDNS()
        {
            string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\DNSPost.txt");
            string data = await File.ReadAllTextAsync(filepath);

            string uri = $"{GetConnectionInfo()}axis-cgi/network_settings.cgi";

            var credCache = new CredentialCache();
            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                response = await httpClient.PostAsync(new Uri(GetConnectionInfo()), new StringContent(data));
                return await response.Content.ReadAsStringAsync();
            }
            catch (TaskCanceledException)
            {
                // TaskCanceledException may indicate a timeout
                Console.WriteLine("Request timed out.");
                return "Time out (cancelled) - Device responds to ping but cannot be interacted with.";
            }
            catch (HttpRequestException ex)
            {
                // Handle exceptions if the URL is unreachable or other HTTP issues
                Console.WriteLine($"Request failed: {ex.Message}");
                return response.StatusCode.ToString();
            }
        }
        */

        public async Task<string> SetDNS()
        {
            var credCache = new CredentialCache();
            string uri = $"{GetConnectionInfo()}axis-cgi/network_settings.cgi"; // Fill in the URI as needed
            string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\DNSPost.txt");
            string data = await File.ReadAllTextAsync(filepath);

            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            HttpResponseMessage answer = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json"); // Adjust content type if necessary
                answer = await httpClient.PostAsync(new Uri(uri), content);
                return await answer.Content.ReadAsStringAsync();
            }
            catch (TaskCanceledException)
            {
                // TaskCanceledException may indicate a timeout
                Console.WriteLine("Request timed out.");
                return "Time out (cancelled) - Device responds to ping but cannot be interacted with.";
            }
            catch (HttpRequestException)
            {
                // Handle exceptions if the URL is unreachable
                Console.WriteLine("Request failed.");
                return answer.StatusCode.ToString();
            }
        }

        public async Task<string> SetNTP()
        {
            var credCache = new CredentialCache();
            string uri = $"{GetConnectionInfo()}axis-cgi/ntp.cgi"; // Fill in the URI as needed
            string filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\NTPPost.txt");
            string data = await File.ReadAllTextAsync(filepath);

            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            HttpResponseMessage answer = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json"); // Adjust content type if necessary
                answer = await httpClient.PostAsync(new Uri(uri), content);
                return await answer.Content.ReadAsStringAsync();
            }
            catch (TaskCanceledException)
            {
                // TaskCanceledException may indicate a timeout
                Console.WriteLine("Request timed out.");
                return "Time out (cancelled) - Device responds to ping but cannot be interacted with.";
            }
            catch (HttpRequestException)
            {
                // Handle exceptions if the URL is unreachable
                Console.WriteLine("Request failed.");
                return answer.StatusCode.ToString();
            }
        }

        public async Task<string> ToggleAnalytics(string analytic, bool state = true)
        {
            var credCache = new CredentialCache();
            string uri = GetApplicationToggle(state, analytic);
            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            HttpResponseMessage answer = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                answer = await httpClient.GetAsync(new Uri(uri));
                return answer.Content.ReadAsStringAsync().Result;
            }
            catch (TaskCanceledException)
            {
                // TaskCanceledException may indicate a timeout
                Console.WriteLine("Request timed out.");
                return "Time out (cancelled) - Device Responds to ping but would cannot be interacted with";
            }
            catch (HttpRequestException)
            {
                // Handle exceptions if the URL is unreachable
                Console.WriteLine("Request timed out.");
                return answer.StatusCode.ToString();
            }
        }

        public async Task<string> GetDeviceProperty(string propertyName)
        {
            // Build the endpoint URL based on the device's IP and port.
            string uri = $"{GetConnectionInfo()}axis-cgi/basicdeviceinfo.cgi";
            // JSON payload to get all properties.
            string payload = @"{
        ""apiVersion"":""1.0"",
        ""method"":""getAllProperties""
    }";

            var credCache = new CredentialCache();
            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });

            try
            {
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(uri), content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: Received status code {response.StatusCode}");
                    return $"Error: {response.StatusCode}";
                }

                // Read the response as a byte array.
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                // Detect the character set.
                var charset = response.Content.Headers.ContentType?.CharSet;
                if (!string.IsNullOrWhiteSpace(charset) && charset.Equals("utf8", StringComparison.OrdinalIgnoreCase))
                {
                    charset = "utf-8";
                }
                Encoding encoding;
                try
                {
                    encoding = !string.IsNullOrWhiteSpace(charset) ? Encoding.GetEncoding(charset) : Encoding.UTF8;
                }
                catch
                {
                    encoding = Encoding.UTF8;
                }
                string jsonResponse = encoding.GetString(bytes);

                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    Console.WriteLine("Empty response received.");
                    return "Empty response";
                }

                // Parse the JSON response to extract the requested property.
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty("data", out JsonElement dataElement) &&
                        dataElement.TryGetProperty("propertyList", out JsonElement propertyList) &&
                        propertyList.TryGetProperty(propertyName, out JsonElement propertyElement))
                    {
                        return propertyElement.GetString();
                    }
                    else
                    {
                        return $"{propertyName} not found";
                    }
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Request timed out.");
                return "Time out (cancelled)";
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed: " + ex.Message);
                return "Request failed";
            }
        }

        public async Task<string> UploadFirmware(string firmwareFilePath, Action<long, long> progressCallback, Action<string> pollCallback)
        {
            // Build the endpoint URL.
            string uri = $"{GetConnectionInfo()}axis-cgi/firmwaremanagement.cgi";

            // Create the multipart/form-data content.
            MultipartFormDataContent multipartContent = new MultipartFormDataContent();

            // Add the "data" form field.
            string jsonData = @"{
      ""apiVersion"": ""1.0"",
      ""context"": ""FirmwareManager"",
      ""method"": ""upgrade""
    }";
            multipartContent.Add(new StringContent(jsonData, Encoding.UTF8, "application/json"), "data");

            // Read the firmware file.
            byte[] fileBytes = await File.ReadAllBytesAsync(firmwareFilePath);
            ByteArrayContent fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            // Use a non-empty key ("file") for the file part.
            multipartContent.Add(fileContent, "file", Path.GetFileName(firmwareFilePath));

            // Buffer the multipart content into a MemoryStream.
            MemoryStream bufferStream = new MemoryStream();
            await multipartContent.CopyToAsync(bufferStream);
            bufferStream.Position = 0;

            // Wrap the buffered stream with a ProgressStream (see next section).
            var progressStream = new ProgressStream(bufferStream, bufferStream.Length, progressCallback);

            // Create a new StreamContent from the ProgressStream.
            var bufferedContent = new StreamContent(progressStream);
            foreach (var header in multipartContent.Headers)
            {
                bufferedContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            bufferedContent.Headers.ContentLength = bufferStream.Length;

            // Set up a new HTTP client with digest authentication.
            var credCache = new CredentialCache();
            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            httpClient.Timeout = TimeSpan.FromMinutes(10);

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(uri), bufferedContent);

                // Read and decode the response.
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                var charset = response.Content.Headers.ContentType?.CharSet;
                if (!string.IsNullOrWhiteSpace(charset) && charset.Equals("utf8", StringComparison.OrdinalIgnoreCase))
                    charset = "utf-8";
                Encoding encoding;
                try { encoding = !string.IsNullOrWhiteSpace(charset) ? Encoding.GetEncoding(charset) : Encoding.UTF8; }
                catch { encoding = Encoding.UTF8; }
                string responseString = encoding.GetString(bytes);

                // Parse the JSON response.
                using (JsonDocument doc = JsonDocument.Parse(responseString))
                {
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty("error", out JsonElement errorElement))
                    {
                        int errorCode = errorElement.GetProperty("code").GetInt32();
                        string errorMessage = errorElement.GetProperty("message").GetString();
                        string errorLog = $"Firmware upload error. Code: {errorCode}, Message: {errorMessage}";
                        Console.WriteLine(errorLog);
                        return errorLog;
                    }
                    else if (root.TryGetProperty("data", out JsonElement dataElement) &&
                             dataElement.TryGetProperty("firmwareVersion", out JsonElement versionElement))
                    {
                        string uploadedVersion = versionElement.GetString();
                        string successLog = $"Firmware upload initiated. Expected version: {uploadedVersion}";
                        Console.WriteLine(successLog);

                        // Log completion of upload.
                        pollCallback?.Invoke("Firmware upload complete. Waiting for device to reboot...");

                        if (Program.WaitForFirmwareUpdate)
                        {
                            DateTime startTime = DateTime.Now;
                            bool updated = false;
                            int attempt = 0;
                            while ((DateTime.Now - startTime) < TimeSpan.FromMinutes(20))
                            {
                                attempt++;
                                pollCallback?.Invoke($"Polling device {DeviceName}: attempt {attempt}");
                                await Task.Delay(TimeSpan.FromSeconds(30));
                                if (await this.Poke(TimeSpan.FromSeconds(5)))
                                {
                                    string currentVersion = await this.GetDeviceProperty("Version");
                                    if (currentVersion == uploadedVersion)
                                    {
                                        updated = true;
                                        break;
                                    }
                                }
                            }
                            return updated
                                ? $"Firmware upgrade successful. Device now running version {uploadedVersion}."
                                : $"Firmware upgrade initiated but device did not report new firmware within the timeout period.";
                        }
                        else
                        {
                            return successLog + " Device is rebooting.";
                        }
                    }
                    else
                    {
                        return "Unexpected response format.";
                    }
                }
            }
            catch (TaskCanceledException)
            {
                return "Firmware upload timed out.";
            }
            catch (HttpRequestException ex)
            {
                return "Firmware upload failed: " + ex.Message;
            }
        }

        public async Task<(bool isReady, string uptimeFormatted)> CheckSystemReady()
        {
            string uri = $"{GetConnectionInfo()}axis-cgi/systemready.cgi";
            string payload = @"{
        ""apiVersion"": ""1.0"",
        ""method"": ""systemready"",
        ""params"": { ""timeout"": 1 }
    }";

            var credCache = new CredentialCache();
            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            try
            {
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(new Uri(uri), content);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Systemready error: HTTP {response.StatusCode}");
                    return (false, "");
                }

                // Read the response as a byte array and decode it properly.
                byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                var charset = response.Content.Headers.ContentType?.CharSet;
                if (!string.IsNullOrWhiteSpace(charset) && charset.Equals("utf8", StringComparison.OrdinalIgnoreCase))
                {
                    charset = "utf-8";
                }
                Encoding encoding;
                try
                {
                    encoding = !string.IsNullOrWhiteSpace(charset) ? Encoding.GetEncoding(charset) : Encoding.UTF8;
                }
                catch
                {
                    encoding = Encoding.UTF8;
                }
                string jsonResponse = encoding.GetString(bytes);

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty("data", out JsonElement dataElement))
                    {
                        // Read the 'systemready' flag.
                        string systemReady = dataElement.GetProperty("systemready").GetString();
                        // Read the uptime (in seconds) as a string.
                        string uptimeStr = dataElement.GetProperty("uptime").GetString();
                        long uptimeSeconds = 0;
                        if (!long.TryParse(uptimeStr, out uptimeSeconds))
                        {
                            uptimeSeconds = 0;
                        }
                        string formattedUptime = uptimeSeconds.ToHumanReadableTime();
                        // Update the device property.
                        this.Uptime = formattedUptime;
                        bool isReady = systemReady.Equals("yes", StringComparison.OrdinalIgnoreCase);
                        return (isReady, formattedUptime);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CheckSystemReady exception: " + ex.Message);
            }
            return (false, "");
        }


        /// <summary>
        /// Checks whether certain critical applications are running on the device.
        /// Logs a message via the supplied callback for any app that is not running.
        /// </summary>
        /// <param name="logCallback">A callback to log messages (e.g. an action that writes to a log window or console).</param>
        public async Task CheckApplications(Action<string> logCallback)
        {
            // Build the endpoint URL for applications/list.cgi.
            string uri = $"{GetConnectionInfo()}axis-cgi/applications/list.cgi";

            var credCache = new CredentialCache();
            credCache.Add(new Uri(GetConnectionInfo()), "Digest", new NetworkCredential(Username, Password));
            var httpClient = new HttpClient(new HttpClientHandler { Credentials = credCache });
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(uri));
                if (!response.IsSuccessStatusCode)
                {
                    logCallback?.Invoke($"CheckApplications: HTTP error {response.StatusCode}");
                    return;
                }

                string responseString = await response.Content.ReadAsStringAsync();

                // Parse the XML response.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseString);
                XmlNodeList appNodes = xmlDoc.GetElementsByTagName("application");

                // Reset previous statuses.
                this.VmdStatus = "Unknown";
                this.ObjectAnalyticsStatus = "Unknown";

                foreach (XmlNode node in appNodes)
                {
                    if (node.Attributes == null)
                        continue;

                    string appName = node.Attributes["Name"]?.Value ?? "";
                    string appID = node.Attributes["ApplicationID"]?.Value ?? "";
                    string appStatus = node.Attributes["Status"]?.Value ?? "";

                    // Check object analytics: expecting Name "objectanalytics" with ID "412806"
                    if (appName.Equals("objectanalytics", StringComparison.OrdinalIgnoreCase) && appID == "412806")
                    {
                        if (!appStatus.Equals("Running", StringComparison.OrdinalIgnoreCase))
                        {
                            await ToggleAnalytics(appName, true);
                            ObjectAnalyticsStatus = "Restarted";
                            logCallback?.Invoke($"Warning: objectanalytics (ID: 412806) not running (Status: {appStatus}). Restarted.");
                        }
                        else
                        {
                            ObjectAnalyticsStatus = "Running";
                            logCallback?.Invoke("objectanalytics is running.");
                        }
                    }

                    // Check vmd: expecting Name "vmd" with ID "143440"
                    if (appName.Equals("vmd", StringComparison.OrdinalIgnoreCase) && appID == "143440")
                    {
                        if (!appStatus.Equals("Running", StringComparison.OrdinalIgnoreCase))
                        {
                            await ToggleAnalytics(appName, true);
                            VmdStatus = "Restarted";
                            logCallback?.Invoke($"Warning: vmd (ID: 143440) not running (Status: {appStatus}). Restarted.");
                        }
                        else
                        {
                            VmdStatus = "Running";
                            logCallback?.Invoke("vmd is running.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logCallback?.Invoke("CheckApplications exception: " + ex.Message);
            }
        }

    }
}