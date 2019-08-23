using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    System.DateTime NextEnergyGain;
    System.DateTime NextDailyReward;
    int Energy = 0;
    bool dayReward = false;
    System.DateTime sysTime;
    public System.DateTime now;
    public UnityEngine.UI.Text Energyt;
    public GameObject EnergyBox;

    bool PauseChk = false;

    // Start is called before the first frame update
    void Start()
    {
        sysTime = GetNetworkTime();
        TimeDifCheck(sysTime);
        StartCoroutine(Sec());
    }

    IEnumerator OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PauseChk = true;
            Debug.Log("일시정지");
            PlayerPrefs.SetString("접속체크시간", now.ToBinary().ToString());
            PlayerPrefs.Save();
        }
        else if (PauseChk)
        {
            PauseChk = false;
            Debug.Log("게임재개");
            yield return new WaitForFixedUpdate();
            TimeDifCheck(sysTime.AddSeconds(Time.unscaledTime));
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("종료");
        PlayerPrefs.SetString("접속체크시간", now.ToBinary().ToString());
        PlayerPrefs.Save();
    }


    IEnumerator Sec()
    {
        while (true)
        {
            now = sysTime.AddSeconds(Time.unscaledTime);
            if (Energy < 20)
            {
                EnergyBox.SetActive(true);
                int remain = 15 - now.Minute % 15;
                if (now.Second == 0)
                    Singleton.Instance.Heal(1);

                if (remain == 15 && now.Second == 0)
                {
                    Singleton.Instance.EnergyGain(1);
                    if (PlayerPrefs.GetInt("에너지") >= 20)
                        EnergyBox.SetActive(false);

                }
                else
                {
                    string m;
                    if ((now.AddMinutes(remain - 1) - now).Minutes < 10)
                        m = "0" + (now.AddMinutes(remain - 1) - now).Minutes.ToString();
                    else
                        m = (now.AddMinutes(remain - 1) - now).Minutes.ToString();

                    string s;
                    if (59 - now.Second < 10)
                        s = "0" + (59 - now.Second).ToString();
                    else
                        s = (59 - now.Second).ToString();
                    Energyt.text = m + ":" + s;
                }
            }
            Debug.Log(now);
            yield return new WaitForSecondsRealtime(Time.timeScale - Time.unscaledDeltaTime);
        }
    }


    public void TimeDifCheck(System.DateTime t)
    {
        now = t;

        //인터넷 시간을 가져오지 못할 경우 기기 시간으로 참조
        //만일 해킹이 우려된다면 이 예외 사항을 없애면 될 듯....
        if (now.Year < 2000)
        {
            now = System.DateTime.Now;
        }

        //키가 없다면 최초 접속으로 판단
        if (PlayerPrefs.HasKey("접속체크시간") == false)
        {
            PlayerPrefs.SetString("접속체크시간", now.ToBinary().ToString());
            PlayerPrefs.SetInt("연속출석", PlayerPrefs.GetInt("연속출석") + 1);  //연속 출석일 수, 따로 저장
            dayReward = true;
            return;
        }
        else
        {
            long lastTime = System.Convert.ToInt64(PlayerPrefs.GetString("접속체크시간"));
            System.DateTime oldDate = System.DateTime.FromBinary(lastTime);

            //그냥 바로 가져온 시간대로 계산하면 시간적으로 24시간이 넘지 않으면 0일로 계산
            //따라서 날짜만 남기고 시간 부분을 초기화 하여 계산해야 정확히 지난 날짜를 산출할 수 있음
            System.DateTime NowTime = new System.DateTime(now.Year, now.Month, now.Day);
            NextDailyReward = NowTime.AddDays(1);
            System.DateTime OldTime = new System.DateTime(oldDate.Year, oldDate.Month, oldDate.Day);

            System.TimeSpan resultTime = NowTime - OldTime;

            int diffDay = resultTime.Days;

            if (diffDay == 1)
            {
                PlayerPrefs.SetInt("연속출석", PlayerPrefs.GetInt("연속출석") + 1);
                dayReward = true; //출석 보상을 지급
            }
            else if (diffDay > 1)
            {
                PlayerPrefs.SetInt("연속출석", PlayerPrefs.GetInt("연속출석") + 1);
            }

            OldTime = oldDate.AddSeconds(-oldDate.Second);
            resultTime = now - OldTime;
            int difMin = (int)(resultTime.TotalMinutes);
            Debug.Log("시간차 : " + difMin);
            Singleton.Instance.Heal(difMin);
            resultTime = now - OldTime.AddMinutes(-OldTime.Minute % 15);
            difMin = (int)(resultTime.TotalMinutes);
            Singleton.Instance.EnergyGain(difMin / 15);

            PlayerPrefs.SetString("접속체크시간", now.ToBinary().ToString());
        }
    }

    public static System.DateTime GetNetworkTime()
    {
        try
        {
            //시간을 참조할 인터넷 사이트
            const string ntpServer = "time.google.com";

            var ntpData = new byte[48];
            ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.ReceiveTimeout = 3000;

            socket.Connect(ipEndPoint);
            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();

            ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
            ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            var networkDateTime = (new System.DateTime(1900, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
            Debug.Log(networkDateTime);
            return networkDateTime.ToLocalTime();
        }
        catch
        {
            //오류가 발생 했을 경우 디폴트 시간으로 넘겨줌
            return new System.DateTime(1900, 1, 1);
        }
    }

}