using System;
using System.Globalization;

namespace IBBAV.Functions
{
	public class DateAndTime
	{
		public DateAndTime()
		{
		}

		public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2)
		{
			long num = DateAndTime.DateDiff(interval, dt1, dt2, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
			return num;
		}

		public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
		{
			long month;
			if (interval == DateInterval.Year)
			{
				month = (long)(dt2.Year - dt1.Year);
			}
			else if (interval != DateInterval.Month)
			{
				TimeSpan timeSpan = dt2 - dt1;
				if ((interval == DateInterval.Day ? true : interval == DateInterval.DayOfYear))
				{
					month = DateAndTime.Round(timeSpan.TotalDays);
				}
				else if (interval == DateInterval.Hour)
				{
					month = DateAndTime.Round(timeSpan.TotalHours);
				}
				else if (interval == DateInterval.Minute)
				{
					month = DateAndTime.Round(timeSpan.TotalMinutes);
				}
				else if (interval == DateInterval.Second)
				{
					month = DateAndTime.Round(timeSpan.TotalSeconds);
				}
				else if (interval == DateInterval.Weekday)
				{
					month = DateAndTime.Round(timeSpan.TotalDays / 7);
				}
				else if (interval == DateInterval.WeekOfYear)
				{
					while (dt2.DayOfWeek != eFirstDayOfWeek)
					{
						dt2 = dt2.AddDays(-1);
					}
					while (dt1.DayOfWeek != eFirstDayOfWeek)
					{
						dt1 = dt1.AddDays(-1);
					}
					timeSpan = dt2 - dt1;
					month = DateAndTime.Round(timeSpan.TotalDays / 7);
				}
				else if (interval == DateInterval.Quarter)
				{
					double quarter = (double)DateAndTime.GetQuarter(dt1.Month);
					double num = (double)DateAndTime.GetQuarter(dt2.Month);
					double num1 = num - quarter;
					double year = (double)(4 * (dt2.Year - dt1.Year));
					month = DateAndTime.Round(num1 + year);
				}
				else
				{
					month = (long)0;
				}
			}
			else
			{
				month = (long)(dt2.Month - dt1.Month + 12 * (dt2.Year - dt1.Year));
			}
			return month;
		}

		private static int GetQuarter(int nMonth)
		{
			int num;
			if (nMonth <= 3)
			{
				num = 1;
			}
			else if (nMonth > 6)
			{
				num = (nMonth > 9 ? 4 : 3);
			}
			else
			{
				num = 2;
			}
			return num;
		}

		private static long Round(double dVal)
		{
			long num;
			num = (dVal < 0 ? (long)Math.Ceiling(dVal) : (long)Math.Floor(dVal));
			return num;
		}
	}
}