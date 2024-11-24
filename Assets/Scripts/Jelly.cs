using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Jelly : MonoBehaviour
{
    // ������ �ĺ��ڿ� ���� ������ �����ϴ� ������
    public int id; // ������ ���� ID
    public int level; // ������ ����
    public float exp; // ������ ���� ����ġ

    public float required_exp; // ���� ���� �ʿ��� ����ġ
    public float max_exp; // �ִ� ����ġ

    // ���� �� �ٸ� ������Ʈ����� ��ȣ �ۿ��� ���� ������
    public GameObject game_manager_obj; // GameManager ������Ʈ ����
    public GameManager game_manager; // GameManager ��ũ��Ʈ ����
    public GameObject left_top; // ���� �� ��� ������Ʈ
    public GameObject right_bottom; // ������ �Ʒ� ��� ������Ʈ

    // ������ �ð��� ǥ���� ���� ������Ʈ��
    public SpriteRenderer sprite_renderer; // ��������Ʈ ������ ������Ʈ
    public Animator anim; // �ִϸ����� ������Ʈ

    float pick_time; // ���콺 Ŭ�� �ð� ���� ����

    // �̵� ���� ������
    int move_delay; // �̵� ��� �ð�
    int move_time; // ���� �̵� �ð�

    float speed_x; // x�� �̵� �ӵ�
    float speed_y; // y�� �̵� �ӵ�

    // bool isWandering; // �������� �����̰� �ִ��� ����
    // bool isWalking; // �Ȱ� �ִ��� ����

    // �׸��� ������Ʈ ���� ������
    GameObject shadow; // �׸��� ������Ʈ
    float shadow_pos_y; // �׸����� y��ǥ

    // ����ƾ�� ȹ���ϴ� ó���� ���� ����
    int jelatin_delay; // ����ƾ ȹ�� ���� �ð�
    bool isGetting; // ����ƾ�� ȹ�� ������ ����

    // ���� �̵�
    public float moveSpeed = 1f; // ������Ʈ�� �̵��ϴ� �ӵ�
    private bool movingDown = true; // ���� �̵� ������ �Ʒ������� ����

    // ���� ���
    private bool isWaiting = false; // ��� ���¸� üũ�ϴ� ����
    public float minWaitTime = 4f;
    public float maxWaitTime = 7f;

    // ���� ���� �� �ʿ��� ������ ������Ʈ�� �����ϴ� �ʱ�ȭ �Լ�
    void Awake()
    {
        // ���� ���� ������ �Ʒ��� ��� ������Ʈ�� ã��
        left_top = GameObject.Find("LeftTop").gameObject;
        right_bottom = GameObject.Find("RightBottom").gameObject;
        // GameManager ������Ʈ�� ã��, �ش� ��ũ��Ʈ�� ����
        game_manager_obj = GameObject.Find("GameManager").gameObject;
        game_manager = game_manager_obj.GetComponent<GameManager>();

        // SpriteRenderer�� Animator ������Ʈ ��������
        sprite_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // �ʱ� �� ����
        // isWandering = false; // ó������ ������ �������� ����
        // isWalking = false;
        isGetting = false; // ó������ ����ƾ�� ȹ������ ����

        // �׸��� ������Ʈ�� ã�� �׸��� ��ġ�� ����
        shadow = transform.Find("Shadow").gameObject;
        switch (id) // ���� ID�� ���� �׸����� y ��ǥ�� �ٸ��� ����
        {
            case 0: shadow_pos_y = -0.05f; break;
            case 6: shadow_pos_y = -0.12f; break;
            case 3: shadow_pos_y = -0.14f; break;
            case 10: shadow_pos_y = -0.16f; break;
            case 11: shadow_pos_y = -0.16f; break;
            default: shadow_pos_y = -0.05f; break;
        }

        // �׸��� ��ġ ����
        shadow.transform.localPosition = new Vector3(0, shadow_pos_y, 0);

        
    }

    // �� �����Ӹ��� ȣ��Ǵ� �Լ���, �ַ� ���� ������Ʈ�� ���
    void Update()
    {
        // ������ ����ġ�� �ִ� ����ġ���� ���� ��, �ð��� ������ ���� ����ġ�� ������Ŵ
        if (exp < max_exp)
            exp += Time.deltaTime;

        // ���� ����ġ�� �������� �ʿ��� ����ġ �̻��̸� ������ ������Ŵ
        if (exp > required_exp * level && level < 3) // ���� 3 ���ϱ����� ����
        {
            game_manager.ChangeAc(anim, ++level); // ������ ������ GameManager���� �ִϸ��̼ǰ� ���� ����
            SoundManager.instance.PlaySound("Grow");
        }

        // ����ƾ�� ���� ȹ������ �ʾҴٸ� ����ƾ ȹ�� �ڷ�ƾ ����
        if (!isGetting)
            StartCoroutine(GetJelatin());


        if (!isWaiting) // ��� ���� �ƴϸ� �̵�
        {
            MoveObject(); // ������Ʈ �̵��� ���� �Լ��� ó��
        }

    }


    // ���� ������Ʈ ó����, ������ �ð� �������� ȣ��� 
    void FixedUpdate()
    {
        /*
        // ������ ���� �������� �����̰� ���� �ʴٸ�, �������� �̵��ϴ� �ڷ�ƾ ����
        if (!isWandering)
            StartCoroutine(Wander());

        // �ȴ� ������ �� �̵� ó��
        if (isWalking)
            Move();

        // ������ ���� ��ġ�� Ȯ���Ͽ� ��踦 ����� �ʵ��� ������ ������Ŵ
        float pos_x = transform.position.x;
        float pos_y = transform.position.y;

        // ����/������ ��踦 ������ x�� ������ ����
        if (pos_x < left_top.transform.position.x || pos_x > right_bottom.transform.position.x)
            speed_x = -speed_x;
        // ����/�Ʒ��� ��踦 ������ y�� ������ ����
        if (pos_y > left_top.transform.position.y || pos_y < right_bottom.transform.position.y)
            speed_y = -speed_y;
        */
    }

    // ���콺 Ŭ�� �� ������ ��ġ�ϴ� �̺�Ʈ ó��
    public void OnMouseDown()
    {
        // ������ ���������� �ʴٸ� �ƹ��� ���۵� ���� ����
        if (!game_manager.isLive) return;

        // �ȴ� ������ ���߰� ��ġ �ִϸ��̼� ����
        // isWalking = false;
        anim.SetBool("isWalk", false);
        anim.SetTrigger("doTouch");

        // ����ġ�� �ִ� ����ġ���� ������ ����ġ�� ������Ŵ
        if (exp < max_exp) ++exp;

        // GameManager�� ����ƾ ȹ�� �̺�Ʈ ����
        game_manager.GetJelatin(id, level);

        SoundManager.instance.PlaySound("Touch");
    }

    // ���콺 �巡�� �� ������ ������� ���� ó��
    void OnMouseDrag()
    {
        // ������ ���� ������ ������ �巡�� ������ �������� ����
        if (!game_manager.isLive) return;

        pick_time += Time.deltaTime; // ���콺 Ŭ�� �ð��� ����

        // Ŭ�� �ð��� �ʹ� ª���� �巡�׸� ó������ ����
        if (pick_time < 0.1f) return;

        // ������ �ȴ� ������ ���߰� ��ġ �ִϸ��̼� ����
        // isWalking = false;
        anim.SetBool("isWalk", false);
        anim.SetTrigger("doTouch");

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ�Ͽ� ������ ��ġ�� �̵�
        Vector3 mouse_pos = Input.mousePosition;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(mouse_pos.x, mouse_pos.y, mouse_pos.y));

        transform.position = point;
    }

    // ���콺�� ������ �� ȣ��Ǵ� �Լ�
    void OnMouseUp()
    {
        // ������ ���� ������ ������ �ƹ��� ���۵� ���� ����
        if (!game_manager.isLive) return;

        pick_time = 0; // Ŭ�� �ð��� �ʱ�ȭ

        // ������ �Ǹ��ϴ� ���̸� ��带 ȹ���ϰ� ������ ����
        if (game_manager.isSell)
        {
            game_manager.GetGold(id, level, this); // ��� ȹ��

            Destroy(gameObject); // ���� ����
        }

        // ������ ��ġ�� ��踦 ����� ��, ������ �ʱ� ��ġ�� �ǵ���
        float pos_x = transform.position.x;
        float pos_y = transform.position.y;

        if (pos_x < left_top.transform.position.x || pos_x > right_bottom.transform.position.x ||
            pos_y > left_top.transform.position.y || pos_y < right_bottom.transform.position.y)
            transform.position = new Vector3(0, -1, 0); // �ʱ� ��ġ�� �ǵ���
    }

    /*
    // ������ �̵� ó�� �Լ�
    void Move()
    {
        // x�� �ӵ��� �����̸� ��������Ʈ�� �¿� �����Ͽ� �̵� ���⿡ ����
        if (speed_x != 0)
            sprite_renderer.flipX = speed_x < 0;

        // ���� ������ �ӵ��� ���� ������ ��ġ�� �̵�
        transform.Translate(speed_x, speed_y, speed_y);
    }

    // ������ ������ �̵��� ó���ϴ� �ڷ�ƾ �Լ�
    IEnumerator Wander()
    {
        // ������ �̵� ��� �ð��� �̵� �ð��� ����
        move_delay = Random.Range(3, 6); // 3~6�� ���� ���
        move_time = Random.Range(3, 6); // 3~6�� ���� �̵�

        // ������ x��� y�� �ӵ��� �������� ����
        speed_x = Random.Range(-0.8f, 0.8f) * Time.deltaTime;
        speed_y = Random.Range(-0.8f, 0.8f) * Time.deltaTime;

        // ������ �������� �̵� ������ ǥ���ϴ� �÷��׸� true�� ����
        isWandering = true;

        // �̵� ��� �ð� ���� ���
        yield return new WaitForSeconds(move_delay);

        // ��� �� �ȴ� ���·� �����ϰ� �ִϸ��̼� ���
        isWalking = true;
        anim.SetBool("isWalk", true); // "isWalk" �ִϸ��̼� Ʈ����

        // ������ �̵� �ð� ���� �̵�
        yield return new WaitForSeconds(move_time);

        // �̵��� ������ �ȴ� ���¸� false�� �����ϰ� �ִϸ��̼� ����
        isWalking = false;
        anim.SetBool("isWalk", false); // "isWalk" �ִϸ��̼� ����

        // ������ �̵��� �������� ǥ��
        isWandering = false;
    }
    */

    // ����ƾ�� �ֱ������� ȹ���ϴ� �ڷ�ƾ �Լ�
    IEnumerator GetJelatin()
    {
        jelatin_delay = 3; // ����ƾ�� ȹ���ϴ� �� �ɸ��� �ð� ����

        // ����ƾ ȹ�� ������ ǥ���ϴ� �÷��׸� true�� ����
        isGetting = true;

        // GameManager���� ����ƾ ȹ�� �Լ� ȣ�� (���� ID�� ���� ����)
        game_manager.GetJelatin(id, level);

        // ����ƾ ȹ�� ���� �ð�(3��) ���� ���
        yield return new WaitForSeconds(jelatin_delay);

        // ����ƾ ȹ���� ������ �÷��׸� false�� ����
        isGetting = false;
    }

    // ������Ʈ�� �̵��� ó���ϴ� �Լ�
    void MoveObject()
    {
        if (movingDown)
        {
            // �Ʒ������� �̵�
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            // y��ǥ�� -1.5�� �����ϸ� 5�� ���
            if (transform.position.y <= -1.5f)
            {
                movingDown = false; // �̵� ������ ���� �ٲ�
                StartCoroutine(WaitAtPosition()); // 5�� ��� ����
            }
        }
        else
        {
            // �������� �̵�
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            // y��ǥ�� 1.5�� �����ϸ� ������Ʈ ����
            if (transform.position.y >= 1.5f)
            {
                Destroy(gameObject); // ������Ʈ �ı�
            }
        }
    }

    // 5�ʰ� ����ϴ� �ڷ�ƾ
    IEnumerator WaitAtPosition()
    {
        float waitTime = Random.Range(minWaitTime, maxWaitTime);

        isWaiting = true; // ��� ���·� ����
        yield return new WaitForSeconds(waitTime); // 4~7�� ���
        isWaiting = false; // ��� ���� �� �ٽ� �̵�
    }
}