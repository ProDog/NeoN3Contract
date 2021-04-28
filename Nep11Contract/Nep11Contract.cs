using Neo;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Native;
using Neo.SmartContract.Framework.Services;
using System.ComponentModel;
using System.Numerics;

namespace Neo3Contract
{
    [DisplayName("MyTestContract")]
    [ManifestExtra("Author", "ProDog")]
    [ManifestExtra("Email", "xxxxxxx")]
    [ManifestExtra("Description", "This is a Neo3 contract example")]
    [SupportedStandards("NEP", "null")]
    [ContractPermission("*", "*")]
    public partial class Nep11Contract : Nep11Token<Nep11TokenState>
    {
        [InitialValue("NUNtEBBbJkmPrmhiVSPN6JuM7AcE8FJ5sE", Neo.SmartContract.ContractParameterType.Hash160)]
        public static UInt160 Owner;

        public override string Symbol()
        {
            return "Neo-Nep11";
        }

        public static bool Verify()
        {
            return Runtime.CheckWitness(Owner);
        }

        public delegate void Notify(params object[] arg);

        [DisplayName("event_name")]
        public static event Notify OnNotify;

        public static void _deploy(object data, bool update)
        {
            if (!update)
            {
                Storage.Put(Storage.CurrentContext, "deploy", 11);
                OnNotify("update", 1);
            }
            else
            {
                Storage.Put(Storage.CurrentContext, "update", 11);
                OnNotify("deploy", 1);
            }
        }

        public static bool Update(ByteString nef, string manifest)
        {
            if (!Runtime.CheckWitness(Owner)) return false;
            ContractManagement.Update(nef, manifest, null);
            return true;
        }

        public static void Mint(UInt160 to)
        {
            ByteString tokenID = NewTokenId();
            Nep11TokenState tokenState = new Nep11TokenState() { Owner = to, Name = "My test token X" };

            Mint(tokenID, tokenState);
        }

        public static void Burn(ByteString tokenID)
        {
            Burn(tokenID);
        }

        public static void OnNEP11Payment(UInt160 from, BigInteger amount, ByteString tokenId, object data)
        {
            OnNotify(tokenId);
            Transfer(from, tokenId, data);
        }

        public static void OnNEP17Payment(UInt160 from, BigInteger amount, object data)
        {
            Mint(from);
        }

        //public static void setvalue()
        //{
        //    Storage.Put(Storage.CurrentContext, "test", "asdklasfd8asda9qw");
        //    Storage.Put(Storage.CurrentContext, "testkey", "ヾ(≧▽≦*)oφ(*￣0￣)（￣︶￣）↗　ψ(｀∇´)ψ(～￣▽￣)～( •̀ ω •́ )✧[]~(￣▽￣)~**^____^*q(≧▽≦q)(～￣▽￣)～<(￣︶￣)↗[GO!]<(￣︶￣)↗[GO!]O(∩_∩)O");
        //}

        //public static object getvalue()
        //{
        //    return Storage.Get(Storage.CurrentContext, "test");
        //}

        //public static string GetData()
        //{
        //    return Storage.Get(Storage.CurrentContext, "testkey");
        //}

        //public static bool ContractTest(byte[] scriptHash, byte[] from, byte[] to, BigInteger amount)
        //{
        //    var result = Contract.Call((UInt160)scriptHash, "transfer", CallFlags.All, new object[] { from, to, amount, null });

        //    OnNotify(result);
        //    OnNotify(scriptHash);

        //    var balance = Contract.Call((UInt160)scriptHash, "balanceOf", CallFlags.All, new object[] { from });
        //    OnNotify(balance);

        //    Contract.Call((UInt160)scriptHash, "transfer", CallFlags.All, new object[] { from, to, amount, null });

        //    var balance1 = Contract.Call((UInt160)scriptHash, "balanceOf", CallFlags.All, new object[] { from });

        //    OnNotify(balance1);

        //    return true;
        //}
    }
}
