using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	public bool isGround;
	public Vector2 groundNormal;
	Vector3 newRight;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;

	Animator animator;

	public bool IsHitLeftObstacle { get; private set; }
	public bool IsHitRightObstacle { get; private set; }

	// Use this for initialization
	void Start()
	{
		isGround = false;
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		groundTest();   // 檢測角色是否正在地面？
		alignSurface(); // 讓角色對齊碰觸的地面
		jumpPose();     // 依據角色狀態播放跳躍動態
	}

	void alignSurface()
	{
		if (isGround && rb.velocity.y > 0) return;
		Vector3 normal = getGroundSurface().normal;
		Vector3 newDir = Vector3.RotateTowards(transform.up, normal, 0.15f, 0.0F);
		transform.rotation = Quaternion.FromToRotation(Vector3.up, newDir);
	}

	void jumpPose()
	{
		if (isGround && !animator.GetCurrentAnimatorStateInfo(0).IsName("player@run")) animator.Play("player@idle");// 如果角色在地面並且未播放跑步動作,則播放待機動作
		if (!isGround) animator.Play("player@jump");// 如果角色在空中,則播放跳躍動作
	}

	void groundTest()
	{
		isGround = getGroundSurface() ? true : false;
	}

	RaycastHit2D getGroundSurface()
	{
		RaycastHit2D raycasthit2d = new RaycastHit2D();
		raycasthit2d.normal = Vector3.up; //懸浮在空中時，法面朝正上方
		Vector3 dir = -transform.up; //地板偵測射線的方向
		RaycastHit2D[] raycasthit2ds = Physics2D.RaycastAll(transform.position, dir, 0.5f);
		foreach (var i in raycasthit2ds) if (i.transform != transform) raycasthit2d = i;
		return raycasthit2d;
	}

	public void goLeft()
	{
		if (IsHitLeftObstacle) return;
		rb.velocity = new Vector2(transform.right.x * -3f, rb.velocity.y);
		spriteRenderer.flipX = true;
		if (isGround) animator.Play("player@run");
	}
	public void goRight()
	{
		if (IsHitRightObstacle) return;
		rb.velocity = new Vector2(transform.right.x * 3f, rb.velocity.y);
		spriteRenderer.flipX = false;
		if (isGround) animator.Play("player@run");
	}
	public void goJump()
	{
		if (isGround) rb.AddForce(Vector2.up * 400);
	}
	public void release()
	{
		if (rb.velocity.y < 0) return;
		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f); //放開空白鍵 減緩跳躍上升
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (isGround) return;
		if (other.transform.position.x < transform.position.x) IsHitLeftObstacle = true;
		IsHitRightObstacle = true;
	}
	void OnCollisionExit2D(Collision2D other)
	{
		IsHitRightObstacle = IsHitLeftObstacle = false;
	}
}
