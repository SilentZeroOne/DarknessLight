using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private Image image;
    private Text names;
    protected Text effect;
    private Text price;
    protected Text totalPrice;
    protected InputField inputField;
    private Button buyBtn;

    private ObjectInfo objectInfo;
    private DrugInfo drugInfo;
    public ObjectInfo ObjectInfo
    {
        get { return objectInfo; }
    }
    public DrugInfo DrugInfo
    {
        get { return drugInfo; }
    }

    private void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
        names = transform.Find("name").GetComponent<Text>();
        effect = transform.Find("effect").GetComponent<Text>();
        price = transform.Find("price").GetComponent<Text>();
        inputField = transform.Find("InputField").GetComponent<InputField>();
        buyBtn = transform.Find("buy").GetComponent<Button>();
        

        buyBtn.onClick.AddListener(PrintNumberToBuy);
        inputField.onEndEdit.AddListener(delegate
        {
            CheckValue(inputField.text);
        });
        inputField.text = "0";
    }
    private void Start()
    {
        totalPrice = transform.root.Find("DrugShop").Find("Image").Find("Panel").Find("totalPrice").GetComponent<Text>();
    }

    public virtual void SetShopItemInfo(ObjectInfo objectInfo)
    {
        this.objectInfo = objectInfo;
        image.sprite = Resources.Load<Sprite>(objectInfo.spritePath);
        names.text = objectInfo.name;
        price.text = objectInfo.price_buy.ToString();
    }
    public virtual void SetShopItemInfo(DrugInfo drugInfo)
    {
        this.drugInfo = drugInfo;
        image.sprite = Resources.Load<Sprite>(drugInfo.spritePath);
        names.text = drugInfo.name;
        price.text = drugInfo.price_buy.ToString();
    }

    public virtual void PrintNumberToBuy()
    {

    }

    public void CheckValue(string text)
    {
        if (!IsNumeric(text))
        {
            inputField.text = "0";
            return;
        }
        if (int.Parse(text) > 9999)
        {
            inputField.text = "9999";
        }
    }
    bool IsNumeric(string str) //接收一个string类型的参数,保存到str里
    {
        if (str == null || str.Length == 0)    //验证这个参数是否为空
            return false;                           //是，就返回False
        ASCIIEncoding ascii = new ASCIIEncoding();//new ASCIIEncoding 的实例
        byte[] bytestr = ascii.GetBytes(str);         //把string类型的参数保存到数组里

        foreach (byte c in bytestr)                   //遍历这个数组里的内容
        {
            if (c < 48 || c > 57)                          //判断是否为数字
            {
                return false;                              //不是，就返回False
            }
        }
        return true;                                        //是，就返回True
    }


    public void ClearNum()
    {
        DrugInfo.buyNum = 0;
        inputField.text = "0";
    }
}
