namespace SimpleSignin.ViewModels;

public class HomeViewModel : BaseViewModel
{
	string? countrySignin = "+92";
	public string? CountrySignin
	{
		get => countrySignin;
		set
		{
			if (countrySignin == value) { return; }
			countrySignin = value;
			OnPropertyChanged();
		}
	}

	string phoneSignin = string.Empty;
	public string PhoneSignin
	{
		get => phoneSignin;
		set
		{
			if (phoneSignin == value) { return; }
			phoneSignin = value;
			OnPropertyChanged();
		}
	}

	string emailSignin = string.Empty;
	public string EmailSignin
	{
		get => emailSignin;
		set
		{
			if (emailSignin == value) { return; }
			emailSignin = value;
			OnPropertyChanged();
		}
	}

	static string? signInMemberId = string.Empty;
	public string? SignInMemberId
	{
		get => signInMemberId;
		set
		{
			if (signInMemberId == value) { return; }
			signInMemberId = value;
			OnPropertyChanged();
		}
	}

	bool isPhoneSignin = true;
	public bool IsPhoneSignin
	{
		get => isPhoneSignin;
		set
		{
			if (isPhoneSignin == value) { return; }
			isPhoneSignin = value;
			OnPropertyChanged();
		}
	}
	bool isEmailSignin = false;
	public bool IsEmailSignin
	{
		get => isEmailSignin;
		set
		{
			if (isEmailSignin == value) { return; }
			isEmailSignin = value;
			OnPropertyChanged();
		}
	}

	bool isGuestSignin = false;
	public bool IsGuestSignin
	{
		get => isGuestSignin;
		set
		{
			if (isGuestSignin == value) { return; }
			isGuestSignin = value;
			OnPropertyChanged();
		}
	}

	bool isSignout = true;
	public bool IsSignout
	{
		get => isSignout;
		set
		{
			if (isSignout == value) { return; }
			isSignout = value;
			OnPropertyChanged();
		}
	}

	public Command SwitchMemberSigninOptionCommand => new Command<string?>((str) =>
	{
		try
		{
			IsBusy = true;
			switch (str)
			{
				case "Phone":
					IsPhoneSignin = true;
					IsEmailSignin = false;
					EmailSignin = string.Empty;
					break;
				case "Email":
					IsPhoneSignin = false;
					IsEmailSignin = true;
					PhoneSignin = string.Empty;
					break;
			}
		}
		catch (Exception ex) { Shell.Current.DisplayAlert("Error:", ex.Message, "OK"); }
		finally
		{
			IsBusy = false;
		}
	}, (str) => IsNotBusy);

	public Command SigninCommand => new Command<string>(async (str) =>
	{
		try
		{
			IsBusy = true;
			string[] substrings = str.Split(',');
			bool isErr = false;

			if (!string.IsNullOrEmpty(PhoneSignin) && !string.IsNullOrEmpty(EmailSignin))
			{
				await Shell.Current.DisplayAlert("Error:", "Please enter either Phone number or Email", "OK");
				isErr = true;
			}
			else
			{
				switch (substrings[1])
				{
					case "Member":
						if (IsPhoneSignin)
						{
							if (string.IsNullOrEmpty(CountrySignin))
							{
								await Shell.Current.DisplayAlert("Error:", "Please select your Phone number's country code", "OK");
								isErr = true;
							}
							else if (string.IsNullOrEmpty(PhoneSignin))
							{
								await Shell.Current.DisplayAlert("Error:", "Please enter your Phone number", "OK");
								isErr = true;
							}
						}
						else if (IsEmailSignin)
						{
							if (string.IsNullOrEmpty(EmailSignin))
							{
								await Shell.Current.DisplayAlert("Error:", "Please enter your Email address", "OK");
								isErr = true;
							}
						}
						if (!isErr)
						{
							SignInMemberId = IsPhoneSignin ? $"{CountrySignin} {PhoneSignin}" : EmailSignin;
							IsSignout = false;
						}
						break;

					case "Guest":
						IsGuestSignin = true;
						IsSignout = false;
						PhoneSignin = string.Empty;
						EmailSignin = string.Empty;
						SignInMemberId = string.Empty;
						break;
				}
			}
		}
		catch (Exception ex) { await Shell.Current.DisplayAlert("Error:", ex.Message, "OK"); }
		finally
		{
			IsBusy = false;
		}
	}, (str) => IsNotBusy);

	public Command SignoutCommand => new Command<string>((str) =>
	{
		IsBusy = true;
		IsGuestSignin = false;
		IsSignout = true;
		IsBusy = false;
	}, (str) => IsNotBusy);
}
