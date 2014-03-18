using UnityEngine;
using System.Collections;

public class BA_Charge : BossAbility
{
	// Use this for initialization
	ChessPiece[,] potentialTargets = new ChessPiece[4, 2];
	BossKnightmare bossKnightmare;
	ChessPiece primaryTarget;
	ChessPiece collateralTarget;
	public int chosenMove = -1;

//Problems:
	//Using King's pawn sacrafice ablity while in check leaves him unable to move
	void Awake ()
		///////////////////////////////////////////////////////////////////////////
	{
		hasCastingCinematic = true;
	}

	override protected void OnStart ()
		///////////////////////////////////////////////////////////////////////////
	{
		abilityName = "Charge";
		canMoveAfterComplete = false;
		bossKnightmare = gameObject.GetComponent<BossKnightmare> ();


	
	}

	override public bool IsReady ()
		///////////////////////////////////////////////////////////////////////////
	{
		//Looks at potential targets, assigns to list appropriately
		potentialTargets [0, 0] = scenario.GetPieceAtXY (bossKnightmare.x, bossKnightmare.y + 1);
		potentialTargets [0, 1] = scenario.GetPieceAtXY (bossKnightmare.x, bossKnightmare.y + 2);
		potentialTargets [1, 0] = scenario.GetPieceAtXY (bossKnightmare.x + 1, bossKnightmare.y);
		potentialTargets [1, 1] = scenario.GetPieceAtXY (bossKnightmare.x + 2, bossKnightmare.y);
		potentialTargets [2, 0] = scenario.GetPieceAtXY (bossKnightmare.x, bossKnightmare.y - 1);
		potentialTargets [2, 1] = scenario.GetPieceAtXY (bossKnightmare.x, bossKnightmare.y - 2);
		potentialTargets [3, 0] = scenario.GetPieceAtXY (bossKnightmare.x - 1, bossKnightmare.y);
		potentialTargets [3, 1] = scenario.GetPieceAtXY (bossKnightmare.x - 2, bossKnightmare.y);
		
		int moveValue = 0;
		int potentialMoveValue = -1;

		//Two space moves
		for (int x = 0; x < 4; x++) {
			int pieceValue1 = 0;
			int pieceValue2 = 0;

			if (potentialTargets [x, 0] != null) {
				pieceValue1 = potentialTargets [x, 0].GetValue ();
				if (potentialTargets [x, 0].playerID == bossKnightmare.playerID)
					pieceValue1 *= -1;
			}

			if (potentialTargets [x, 1] != null) {
				pieceValue2 = potentialTargets [x, 1].GetValue ();
				if (potentialTargets [x, 1].playerID == bossKnightmare.playerID)
					pieceValue2 *= -1;
			}
			potentialMoveValue = pieceValue1 + pieceValue2;
			if (potentialMoveValue > moveValue) {
				moveValue = potentialMoveValue;
				chosenMove = x;
				Debug.Log (chosenMove);
			}
		}

		/*
		bool chosenMoveWillCaptureAlly = false;
		if (potentialTargets[chosenMove, 0] != null)
		{
			if (potentialTargets[chosenMove, 0].playerID == bossKnightmare.playerID) chosenMoveWillCaptureAlly = true;
		}
		*/

		switch (chosenMove) {
		//2 space moves
		case 0:
			primaryTarget = potentialTargets [0, 1];
			collateralTarget = potentialTargets [0, 0];
			break;
			
		case 1:
			primaryTarget = potentialTargets [1, 1];
			collateralTarget = potentialTargets [1, 0];
			break;
			
		case 2:
			primaryTarget = potentialTargets [2, 1];
			collateralTarget = potentialTargets [2, 0];
			break;
			
		case 3:
			primaryTarget = potentialTargets [3, 1];
			collateralTarget = potentialTargets [3, 0];
			break;

		}

		if (chosenMove > -1) {
			Debug.Log ("Ready to go");//----Debug Line, Delete me
			return true;
		} else {
			return false;
		}
	  
	}

	override public void Activate () 
		///////////////////////////////////////////////////////////////////////////
	{
		base.Activate ();
		Debug.Log ("Charge Activated!");
		//gameObject.AddComponent<Rigidbody>();
		//gameObject.rigidbody.isKinematic = true;
		//gameObject.rigidbody.useGravity = false;

		if(primaryTarget != null){
			primaryTarget.PrepareToBeCapturedBy (bossKnightmare, false);
			Debug.Log("PrimaryReadyForCapture");
		}

		if(collateralTarget != null){
			collateralTarget.PrepareToBeCapturedBy (bossKnightmare, false);
			Debug.Log("CollateralReadyForCapture");
		}
			bossKnightmare.MoveToXY (primaryTarget.x, primaryTarget.y, primaryTarget);
	
		/*
			//1 space moves
		case 4:
			targetChessPiece = potentialTargets [0, 0];
			break;
		case 5:
			targetChessPiece = potentialTargets [1, 0];
			break;
		case 6:
			targetChessPiece = potentialTargets [2, 0];
			break;
		case 7:
			targetChessPiece = potentialTargets [3, 0];
			break;
			*/


	}

	override public void OnUpdate ()
		///////////////////////////////////////////////////////////////////////////
	{
	}

	override public void Deactivate ()
		///////////////////////////////////////////////////////////////////////////
	{
		base.Deactivate ();
		primaryTarget = null;
		collateralTarget = null;
		Destroy(transform.GetComponent<Rigidbody>());
	}


	override public PotentialMove GetMove ()
		///////////////////////////////////////////////////////////////////////////
	{
		if (primaryTarget != null) {
			PotentialMove move = new PotentialMove (GetComponent<ChessPiece> (), primaryTarget.GetX (), primaryTarget.GetY (), primaryTarget);
			move.SetBossAbility (true);
			return move;
		} else {
			Debug.Log ("ERROR! " + gameObject.name + " tried to get a potential move on a null target!");
			return null;
		}
	}



}
