using System;

namespace RoWa
{
	namespace Game
	{
		public class GameTime
		{
			public int gtHours { get; private set; }
			public int gtMinutes { get; private set; }
			public int gtSeconds { get; private set; }

			public int gtDays { get; private set; }
			public int gtMonths { get; private set; }
			public int gtYears { get; private set; }

			public int addHours { get; set; }
			public int addMinutes { get; set; }
			public int addSeconds { get; set; }
			public int addDays { get; set; }
			public int addMonths { get; set; }
			public int addYears { get; set; }

			public int interval { get; set; }

			public EventHandler<GameTimeArgs> OnTick { get; set; }

			System.Timers.Timer timer = new System.Timers.Timer();

			/// <summary>
			/// Create a new GameTime
			/// </summary>
			/// <param name="inter">The interval in miliseconds</param>
			/// <param name="autostart">If true, it will start automatic</param>
			public GameTime(int inter = 1000, bool autostart = true)
			{
				gtHours = 0;
				gtMinutes = 0;
				gtSeconds = 0;
				gtDays = 1;
				gtMonths = 1;
				gtYears = 1;

				addSeconds = 1;
				addMinutes = 0;
				addHours = 0;
				addDays = 0;
				addMonths = 0;
				addYears = 0;

				interval = inter;
				timer.Interval = interval;

				if (autostart)
					Start();
			}

			/// <summary>
			/// Create a new GameTime
			/// </summary>
			/// <param name="hours">The gtHours</param>
			/// <param name="minutes">The gtMinutes</param>
			/// <param name="seconds">The gtSeconds</param>
			/// <param name="days">The gtDays</param>
			/// <param name="months">The gtMonths</param>
			/// <param name="years">The gtYears</param>
			/// <param name="inter">The interval in miliseconds</param>
			/// <param name="autostart">If true, it will start automatic</param>
			public GameTime(int hours, int minutes, int seconds, int days, int months, int years, int inter, bool autostart = true)
			{
				gtHours = hours;
				gtMinutes = minutes;
				gtSeconds = seconds;
				gtDays = days;
				gtMonths = months;
				if(years < 1) { years = 1; }
				gtYears = years;

				addSeconds = 1;
				addMinutes = 0;
				addHours = 0;
				addDays = 0;
				addMonths = 0;
				addYears = 0;

				timer.Interval = inter;
				if(autostart)
					Start();
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="hours">The gtHours</param>
			/// <param name="minutes">The gtMinutes</param>
			/// <param name="seconds">The gtSeconds</param>
			/// <param name="days">The gtDays</param>
			/// <param name="months">The gtMonths</param>
			/// <param name="years">The gtYears</param>
			/// <param name="inter">The interval in miliseconds</param>
			/// <param name="addseconds">The seconds to add per tick</param>
			/// <param name="addminutes">The minutes to add per tick</param>
			/// <param name="addhours">The hours to add per tick</param>
			/// <param name="adddays">The days to add per tick</param>
			/// <param name="addmonths">The months to add per tick</param>
			/// <param name="addyears">The years to add per tick</param>
			/// <param name="autostart">If true, it will start automatic</param>
			public GameTime(int hours, int minutes, int seconds, int days, int months, int years, int inter, int addseconds, int addminutes = 0, int addhours = 0, int adddays = 0, int addmonths = 0, int addyears = 0, bool autostart = true)
			{
				gtHours = hours;
				gtMinutes = minutes;
				gtSeconds = seconds;
				if(days < 1) { days = 1; }
				gtDays = days;
				if(months < 1) { months = 1; }
				gtMonths = months;
				gtYears = years;

				addSeconds = addseconds;
				addMinutes = addminutes;
				addHours = addhours;
				addDays = adddays;
				addMonths = addmonths;
				addYears = addyears;

				timer.Interval = inter;
				if (autostart)
					Start();
			}

			/// <summary>
			/// Starts the GameTime.timer
			/// </summary>
			public void Start()
			{
				timer.Elapsed += TimerTicks;
				timer.Start();
			}

			/// <summary>
			/// Stops the GameTime.timer
			/// </summary>
			public void Stop()
			{
				timer.Stop();
			}

			/// <summary>
			/// Is thrown when the GameTime.timer ticks
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="args"></param>
			void TimerTicks(object sender, EventArgs args)
			{
				Tick(addSeconds, addMinutes, addHours, addDays, addMonths, addYears);
			}

			/// <summary>
			/// Manual GameTime.Tick
			/// </summary>
			/// <param name="seconds">Seconds to add</param>
			/// <param name="minutes">Minutes to add</param>
			/// <param name="hours">Hours to add</param>
			/// <param name="days">Days to add</param>
			/// <param name="months">Months to add</param>
			/// <param name="years">Years to add</param>
			public void Tick(int seconds, int minutes = 0, int hours = 0, int days = 0, int months = 0, int years = 0)
			{
				gtSeconds += seconds;
				while(gtSeconds >= 60)
				{
					gtSeconds -= 60;
					minutes++;
				}

				gtMinutes += minutes;
				while(gtMinutes >= 60)
				{
					gtMinutes -= 60;
					hours++;
				}

				gtHours += hours;
				while(gtHours >= 24)
				{
					gtHours -= 24;
					days++;
				}

				gtDays += days;
				while (gtDays > DateTime.DaysInMonth(gtYears, gtMonths))
				{
					gtDays -= DateTime.DaysInMonth(gtYears, gtMonths);
					months++;
				}

				gtMonths += months;
				while(gtMonths > 12)
				{
					gtMonths -= 12;
					years++;
				}

				gtYears += years;

				GameTimeArgs args = new GameTimeArgs(gtHours,gtMinutes,gtSeconds,gtDays,gtMonths,gtYears);
				OnTick?.Invoke(this, args);
			}

			/// <summary>
			/// Returns the GameTime in a specific format
			/// </summary>
			/// <param name="format">The format of the GameTime</param>
			/// <returns></returns>
			public string ToString(string format = "h:m:s d.m.yyyy")
			{
				DateTime dt = new DateTime(gtYears, gtMonths, gtDays, gtHours, gtMinutes, gtSeconds);
				return dt.ToString(format);
			}

			public class GameTimeArgs : EventArgs
			{
				public int hours { get; private set; }
				public int minutes { get; private set; }
				public int seconds { get; private set; }
				public int days { get; private set; }
				public int months { get; private set; }
				public int years { get; private set; }

				public GameTimeArgs(int h, int m, int s, int d, int mo, int y)
				{
					hours = h;
					minutes = m;
					seconds = s;
					days = d;
					months = mo;
					years = y;

				}

				public string ToString(string format = "h:m:s d.m.yyyy")
				{
					DateTime dt = new DateTime(years, months, days, hours, minutes, seconds);
					return dt.ToString(format);
				}
			} 
		}
	}
}
