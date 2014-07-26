using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dialogue sequence.
/// </summary>
public class DialogueSequence {

	public struct Option {
		public string from;
		public string to;
		public string description;

		public static bool operator ==(Option left, Option right) {
			return (left.from == right.from) && (left.to == right.to) && (left.description == right.description);
		}

		public static bool operator !=(Option left, Option right) {
			return !(left == right);
		}
	}
	
	private List<DialogueNode> nodes;
	private List<Option> options;

	public int numberOfNodes {
		get{
			return nodes.Count;
		}
	}
	
	public string startingNodeName {
		get{
			if( FindNode("Start") == null) {
				if(nodes.Count > 0){
					return nodes[0].name;
				}
				else {
					return null;
				}
			}
			else {
				return "Start";
			}
		}
	}

	public DialogueSequence() {
		this.nodes = new List<DialogueNode>();
		this.options = new List<Option>();
	}

	public DialogueSequence(TextAsset nodeFile) : this() {
		System.IO.StringReader sr = new System.IO.StringReader(nodeFile.text);

		while(sr.Peek() != -1 && sr.ReadLine().Trim().Equals("NODE:")) {  // Read each node in.
			string name = sr.ReadLine().Trim().Substring(6);
			string speaker = sr.ReadLine().Trim().Substring(9);
			string text = sr.ReadLine().Trim().Substring(6);
			string tmp;

			while( (tmp = sr.ReadLine().Trim()) != "ENDTEXT") {
				text += tmp + " ";
			}

			DialogueNode newNode = new DialogueNode(name, speaker, text);

			while( (tmp = sr.ReadLine().Trim()) == "OPTION:" ) {
				name = sr.ReadLine().Trim();
				text = sr.ReadLine().Trim();
				options.Add( new Option{ from = newNode.name, to = name, description = text } );
			}
			this.AddNode(newNode);
		}

		//TODO Verify that all options are valid.
	}

	public string GetText(string node) {
		DialogueNode tmp = FindNode(node);
		if(tmp != null) {
			return tmp.text;
		}
		else {
			return null;
		}
	}

	public string GetSpeaker(string node) {
		DialogueNode tmp = FindNode(node);
		if(tmp != null) {
			return tmp.speaker;
		}
		else {
			return null;
		}
	}

	/// <summary>
	/// Gets the node with the given name. This method should only be used
	/// when the returned node needs to be modified; the string based methods of this
	/// class are the intended interface for read-only operations.
	/// </summary>
	/// <returns>The node.</returns>
	/// <param name="name">The name of the node to search for.</param>
	public DialogueNode GetNode(string name) {
		return FindNode(name);
	}

	public bool AddNode(DialogueNode node) {
		nodes.Add(node);
		return true;
	}

	public bool AddNode(string name, string speaker, string text) {
		return AddNode( new DialogueNode(name, speaker, text) );
	}

	private DialogueNode FindNode(string name) {
		for(int i = 0; i < this.nodes.Count; i++) {
			if(this.nodes[i].name == name)
				return this.nodes[i];
		}

		return null;
	}

	public bool AddOption(string fromNode, string toNode, string optText) {
		if( FindNode(fromNode) == null || FindNode(toNode) == null) {
			return false;
		}
		else {
			options.Add( new Option { from = fromNode, to = toNode, description = optText } );
			return true;
		}
	}

	public bool RemoveOption(Option opt) {
		for(int i = 0; i < options.Count; i++) {
			if(options[i] == opt) {
				options.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	public bool RemoveNode(string name) {
		bool rv = false;
		for(int i = 0; i < nodes.Count; i++) {
			if(nodes[i].name == name) {
				nodes.RemoveAt(i);
				rv = true;
				break;
			}
		}

		if(rv) {
			for(int i = 0; i < options.Count; i++) {
				if(options[i].to == name || options[i].from == name) {
					options.RemoveAt(i);
				}
			}
		}

		return rv;
	}

	public void ListNodes() {
		Debug.Log("Known nodes: ");
		for(int i = 0; i < nodes.Count; i++)
			Debug.Log(nodes[i].name);
	}

	public List<DialogueSequence.Option> GetOptions(string name) {
		List<Option> opts = new List<Option>();

		for(int i = 0; i < options.Count; i++) {
			if(options[i].from == name) {
				opts.Add(options[i]);
			}
		}

		return opts;
	}


}