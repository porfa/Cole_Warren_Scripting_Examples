using UnityEngine;
using System.Collections;

public class BA_Gloom : BossAbility {

	BossApostate bossApostate;
	ChessPiece targetChessPiece;
	ArrayList potentialTargets;
	public GameObject DepressedEffectParticles;
	//Cycle through potential moves of target chess piece, and move it into a position in which it can be captured.
	void Awake ()
		///////////////////////////////////////////////////////////////////////////
	{
		hasCastingCinematic = true;
	}
	
	override protected void OnStart ()
		///////////////////////////////////////////////////////////////////////////
	{
		abilityName = "Gloom";
		canMoveAfterComplete = true;
		bossApostate = gameObject.GetComponent<BossApostate> ();
		
	}
	
	override public bool IsReady ()
		///////////////////////////////////////////////////////////////////////////
	{
		potentialTargets = new ArrayList ();
		
		
		foreach (ChessPiece player1Piece in scenario.GetAllChessPiecesForPlayer(1)) {
			foreach (ChessPiece player2Piece in scenario.GetAllChessPiecesForPlayer(2)) {
				if (player1Piece.CanCaptureXY (player2Piece.x, player2Piece.y) && !player1Piece.IsDepressed())
				{
					potentialTargets.Add (player1Piece);
				}
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
			return true;	

		
	}
	}
	
	override public void Activate () 
		///////////////////////////////////////////////////////////////////////////
	{
		base.Activate ();
		targetChessPiece.SetPieceEffect(PieceEffect.Depressed);
	}
	
	override public void OnUpdate ()
		///////////////////////////////////////////////////////////////////////////
	{
		if (!isComplete)
		{

			targetChessPiece.SetDepressed(true);
				isComplete = true;
		}
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

