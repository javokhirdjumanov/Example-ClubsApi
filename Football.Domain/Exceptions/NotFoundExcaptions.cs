namespace Football.Domain.Exceptions;
public class NotFoundExcaptions : Exception
{
	public NotFoundExcaptions(string message) 
		: base(message) 
	{ }
}
