using UnityEngine;
using System.Collections;

public class BA_Dominate : BossAbility
{

	BossAgonist bossAgonist;
	ChessPiece targetChessPiece;
	ArrayList potentialPlacesToMoveTarget;
	ArrayList potentialTargets;

	//Cycle through potential moves of target chess piece, and move it into a position in which it can be captured.
	void Awake ()
	///////////////////////////////////////////////////////////////////////////
	{
			hasCastingCinematic = true;
	}

	override protected void OnStart ()
	///////////////////////////////////////////////////////////////////////////
	{
			abilityName = "Dominate";
			canMoveAfterComplete = true;
			bossAgonist = gameObject.GetComponent<BossAgonist> ();

	}

	override public bool IsReady ()
	///////////////////////////////////////////////////////////////////////////
	{
		    potentialTargets = new ArrayList ();


		foreach (ChessPiece player1Piece in scenario.GetAllChessPiecesForPlayer(1)) {
					foreach (ChessPiece player2Piece in scenario.GetAllChessPiecesForPlayer(2)) {
							if (player1Piece.CanCaptureXY (player2Piece.x, player2Piece.y))
									potentialTargets.Add (player1Piece);
					}
			}


			int targetValue = 0;
	
			foreach (ChessPiece piece in potentialTargets) {
					if (piece.GetValue () > targetValue) {
							targetValue = piece.GetValue ();
							targetChessPiece = piece;
					}
			}

			if (targetChessPiece == null) 
		{
			return false;
		}
		else
		{

			 potentialPlacesToMoveTarget = new ArrayList ();


			foreach (PotentialMove move in targetChessPiece.GetMoveList()) 
			{
				//Debug.Log (player2Piece.gameObject.name + ": Has " + player2Piece.GetMoveList().Count + " potential moves");
				//int goodmoves = 0;
				
				foreach (ChessPiece player2Piece in scenario.GetAllChessPiecesForPlayer(2)) 				{
					//Debug.Log ("checked" + counter + "moves");
					
					if (player2Piece.CanCaptureChessPieceIfItMovedToXY (targetChessPiece, move.x, move.y))
					{
						if (GameObject.FindGameObjectWithTag ("Scenario").GetComponent<Scenario> ().GetPieceAtXY (move.x, move.y) == null) 
						{
							//goodmoves++;
							potentialPlacesToMoveTarget.Add (move);
							Debug.Log(potentialPlacesToMoveTarget.Count);

						}
					}
					
				}
				
				
				//	Debug.Log (player2Piece.gameObject.name + ": Potential Places to Move Target: " + goodmoves);
			}
		}
					if(potentialPlacesToMoveTarget.Count > 0){
				return true;
			}
			 else {
					return false;
			}
	}

	override public void Activate () 
	///////////////////////////////////////////////////////////////////////////
	{
		base.Activate ();
		//Debug.Log (gameObject.name + ": I have " + potentialPlacesToMoveTarget.Count + " places I can move the target piece so it can be captured");
		int chosenSpot = (int)UnityEngine.Random.Range (0, potentialPlacesToMoveTarget.Count);
		targetChessPiece.MoveToXY (((PotentialMove)potentialPlacesToMoveTarget [chosenSpot]).x, ((PotentialMove)potentialPlacesToMoveTarget [chosenSpot]).y);
	}

	override public void OnUpdate ()
	///////////////////////////////////////////////////////////////////////////
	{
	}

	override public void Deactivate ()
	///////////////////////////////////////////////////////////////////////////
	{
			base.Deactivate ();
	}
	

	override public PotentialMove GetMove ()
	///////////////////////////////////////////////////////////////////////////
	{
			if (targetChessPiece != null) {
					PotentialMove move = new PotentialMove (GetComponent<ChessPiece> (), targetChessPiece.GetX (), targetChessPiece.GetY (), targetChessPiece);
					move.SetBossAbility (true);
					return move;
			} else {
					Debug.Log ("ERROR! " + gameObject.name + " tried to get a potential move on a null target!");
					return null;
			}
	}

}
