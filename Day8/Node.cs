public class Node
{
	public string Left { get; set; }
	public string Right { get; set; }
	public string Location { get; set; }

	public Node(string location)
	{
		Location = location;
	}
}