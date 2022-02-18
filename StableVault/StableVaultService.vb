Imports System
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Numerics
Imports Nethereum.Hex.HexTypes
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Web3
Imports Nethereum.RPC.Eth.DTOs
Imports Nethereum.Contracts.CQS
Imports Nethereum.Contracts.ContractHandlers
Imports Nethereum.Contracts
Imports System.Threading
Imports StablecoinFun.Contracts.StableVault.ContractDefinition
Namespace StablecoinFun.Contracts.StableVault


    Public Partial Class StableVaultService
    
    
        Public Shared Function DeployContractAndWaitForReceiptAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal stableVaultDeployment As StableVaultDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return web3.Eth.GetContractDeploymentHandler(Of StableVaultDeployment)().SendRequestAndWaitForReceiptAsync(stableVaultDeployment, cancellationTokenSource)
        
        End Function
         Public Shared Function DeployContractAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal stableVaultDeployment As StableVaultDeployment) As Task(Of String)
        
            Return web3.Eth.GetContractDeploymentHandler(Of StableVaultDeployment)().SendRequestAsync(stableVaultDeployment)
        
        End Function
        Public Shared Async Function DeployContractAndGetServiceAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal stableVaultDeployment As StableVaultDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of StableVaultService)
        
            Dim receipt = Await DeployContractAndWaitForReceiptAsync(web3, stableVaultDeployment, cancellationTokenSource)
            Return New StableVaultService(web3, receipt.ContractAddress)
        
        End Function
    
        Protected Property Web3 As Nethereum.Web3.Web3
        
        Public Property ContractHandler As ContractHandler
        
        Public Sub New(ByVal web3 As Nethereum.Web3.Web3, ByVal contractAddress As String)
            Web3 = web3
            ContractHandler = web3.Eth.GetContractHandler(contractAddress)
        End Sub
    
        Public Function DOMAIN_SEPARATORQueryAsync(ByVal dOMAIN_SEPARATORFunction As DOMAIN_SEPARATORFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Byte())
        
            Return ContractHandler.QueryAsync(Of DOMAIN_SEPARATORFunction, Byte())(dOMAIN_SEPARATORFunction, blockParameter)
        
        End Function

        
        Public Function DOMAIN_SEPARATORQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Byte())
        
            return ContractHandler.QueryAsync(Of DOMAIN_SEPARATORFunction, Byte())(Nothing, blockParameter)
        
        End Function



        Public Function MAX_DEBT_RATIOQueryAsync(ByVal mAX_DEBT_RATIOFunction As MAX_DEBT_RATIOFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MAX_DEBT_RATIOFunction, BigInteger)(mAX_DEBT_RATIOFunction, blockParameter)
        
        End Function

        
        Public Function MAX_DEBT_RATIOQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MAX_DEBT_RATIOFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PERMIT_TYPEHASHQueryAsync(ByVal pERMIT_TYPEHASHFunction As PERMIT_TYPEHASHFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Byte())
        
            Return ContractHandler.QueryAsync(Of PERMIT_TYPEHASHFunction, Byte())(pERMIT_TYPEHASHFunction, blockParameter)
        
        End Function

        
        Public Function PERMIT_TYPEHASHQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Byte())
        
            return ContractHandler.QueryAsync(Of PERMIT_TYPEHASHFunction, Byte())(Nothing, blockParameter)
        
        End Function



        Public Function AllowanceQueryAsync(ByVal allowanceFunction As AllowanceFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of AllowanceFunction, BigInteger)(allowanceFunction, blockParameter)
        
        End Function

        
        Public Function AllowanceQueryAsync(ByVal [returnValue1] As String, ByVal [returnValue2] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim allowanceFunction = New AllowanceFunction()
                allowanceFunction.ReturnValue1 = [returnValue1]
                allowanceFunction.ReturnValue2 = [returnValue2]
            
            Return ContractHandler.QueryAsync(Of AllowanceFunction, BigInteger)(allowanceFunction, blockParameter)
        
        End Function


        Public Function ApproveRequestAsync(ByVal approveFunction As ApproveFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of ApproveFunction)(approveFunction)
        
        End Function

        Public Function ApproveRequestAndWaitForReceiptAsync(ByVal approveFunction As ApproveFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of ApproveFunction)(approveFunction, cancellationToken)
        
        End Function

        
        Public Function ApproveRequestAsync(ByVal [spender] As String, ByVal [amount] As BigInteger) As Task(Of String)
        
            Dim approveFunction = New ApproveFunction()
                approveFunction.Spender = [spender]
                approveFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAsync(Of ApproveFunction)(approveFunction)
        
        End Function

        
        Public Function ApproveRequestAndWaitForReceiptAsync(ByVal [spender] As String, ByVal [amount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim approveFunction = New ApproveFunction()
                approveFunction.Spender = [spender]
                approveFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of ApproveFunction)(approveFunction, cancellationToken)
        
        End Function
        Public Function AssetsOfQueryAsync(ByVal assetsOfFunction As AssetsOfFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of AssetsOfFunction, BigInteger)(assetsOfFunction, blockParameter)
        
        End Function

        
        Public Function AssetsOfQueryAsync(ByVal [user] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim assetsOfFunction = New AssetsOfFunction()
                assetsOfFunction.User = [user]
            
            Return ContractHandler.QueryAsync(Of AssetsOfFunction, BigInteger)(assetsOfFunction, blockParameter)
        
        End Function


        Public Function AssetsPerShareQueryAsync(ByVal assetsPerShareFunction As AssetsPerShareFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of AssetsPerShareFunction, BigInteger)(assetsPerShareFunction, blockParameter)
        
        End Function

        
        Public Function AssetsPerShareQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of AssetsPerShareFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function BalanceOfQueryAsync(ByVal balanceOfFunction As BalanceOfFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of BalanceOfFunction, BigInteger)(balanceOfFunction, blockParameter)
        
        End Function

        
        Public Function BalanceOfQueryAsync(ByVal [returnValue1] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim balanceOfFunction = New BalanceOfFunction()
                balanceOfFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of BalanceOfFunction, BigInteger)(balanceOfFunction, blockParameter)
        
        End Function


        Public Function DecimalsQueryAsync(ByVal decimalsFunction As DecimalsFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Byte)
        
            Return ContractHandler.QueryAsync(Of DecimalsFunction, Byte)(decimalsFunction, blockParameter)
        
        End Function

        
        Public Function DecimalsQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Byte)
        
            return ContractHandler.QueryAsync(Of DecimalsFunction, Byte)(Nothing, blockParameter)
        
        End Function



        Public Function DefundRequestAsync(ByVal defundFunction As DefundFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of DefundFunction)(defundFunction)
        
        End Function

        Public Function DefundRequestAndWaitForReceiptAsync(ByVal defundFunction As DefundFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of DefundFunction)(defundFunction, cancellationToken)
        
        End Function

        
        Public Function DefundRequestAsync(ByVal [shares] As BigInteger, ByVal [to] As String, ByVal [from] As String) As Task(Of String)
        
            Dim defundFunction = New DefundFunction()
                defundFunction.Shares = [shares]
                defundFunction.To = [to]
                defundFunction.From = [from]
            
            Return ContractHandler.SendRequestAsync(Of DefundFunction)(defundFunction)
        
        End Function

        
        Public Function DefundRequestAndWaitForReceiptAsync(ByVal [shares] As BigInteger, ByVal [to] As String, ByVal [from] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim defundFunction = New DefundFunction()
                defundFunction.Shares = [shares]
                defundFunction.To = [to]
                defundFunction.From = [from]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of DefundFunction)(defundFunction, cancellationToken)
        
        End Function
        Public Function DepositRequestAsync(ByVal depositFunction As DepositFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of DepositFunction)(depositFunction)
        
        End Function

        Public Function DepositRequestAndWaitForReceiptAsync(ByVal depositFunction As DepositFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of DepositFunction)(depositFunction, cancellationToken)
        
        End Function

        
        Public Function DepositRequestAsync(ByVal [amount] As BigInteger, ByVal [to] As String) As Task(Of String)
        
            Dim depositFunction = New DepositFunction()
                depositFunction.Amount = [amount]
                depositFunction.To = [to]
            
            Return ContractHandler.SendRequestAsync(Of DepositFunction)(depositFunction)
        
        End Function

        
        Public Function DepositRequestAndWaitForReceiptAsync(ByVal [amount] As BigInteger, ByVal [to] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim depositFunction = New DepositFunction()
                depositFunction.Amount = [amount]
                depositFunction.To = [to]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of DepositFunction)(depositFunction, cancellationToken)
        
        End Function
        Public Function FundRequestAsync(ByVal fundFunction As FundFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of FundFunction)(fundFunction)
        
        End Function

        Public Function FundRequestAndWaitForReceiptAsync(ByVal fundFunction As FundFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of FundFunction)(fundFunction, cancellationToken)
        
        End Function

        
        Public Function FundRequestAsync(ByVal [amount] As BigInteger, ByVal [to] As String) As Task(Of String)
        
            Dim fundFunction = New FundFunction()
                fundFunction.Amount = [amount]
                fundFunction.To = [to]
            
            Return ContractHandler.SendRequestAsync(Of FundFunction)(fundFunction)
        
        End Function

        
        Public Function FundRequestAndWaitForReceiptAsync(ByVal [amount] As BigInteger, ByVal [to] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim fundFunction = New FundFunction()
                fundFunction.Amount = [amount]
                fundFunction.To = [to]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of FundFunction)(fundFunction, cancellationToken)
        
        End Function
        Public Function GetLatestPriceQueryAsync(ByVal getLatestPriceFunction As GetLatestPriceFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of GetLatestPriceFunction, BigInteger)(getLatestPriceFunction, blockParameter)
        
        End Function

        
        Public Function GetLatestPriceQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of GetLatestPriceFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MaxDepositQueryAsync(ByVal maxDepositFunction As MaxDepositFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MaxDepositFunction, BigInteger)(maxDepositFunction, blockParameter)
        
        End Function

        
        Public Function MaxDepositQueryAsync(ByVal [returnValue1] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim maxDepositFunction = New MaxDepositFunction()
                maxDepositFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of MaxDepositFunction, BigInteger)(maxDepositFunction, blockParameter)
        
        End Function


        Public Function MaxMintQueryAsync(ByVal maxMintFunction As MaxMintFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MaxMintFunction, BigInteger)(maxMintFunction, blockParameter)
        
        End Function

        
        Public Function MaxMintQueryAsync(ByVal [returnValue1] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim maxMintFunction = New MaxMintFunction()
                maxMintFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of MaxMintFunction, BigInteger)(maxMintFunction, blockParameter)
        
        End Function


        Public Function MaxRedeemQueryAsync(ByVal maxRedeemFunction As MaxRedeemFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MaxRedeemFunction, BigInteger)(maxRedeemFunction, blockParameter)
        
        End Function

        
        Public Function MaxRedeemQueryAsync(ByVal [user] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim maxRedeemFunction = New MaxRedeemFunction()
                maxRedeemFunction.User = [user]
            
            Return ContractHandler.QueryAsync(Of MaxRedeemFunction, BigInteger)(maxRedeemFunction, blockParameter)
        
        End Function


        Public Function MaxWithdrawQueryAsync(ByVal maxWithdrawFunction As MaxWithdrawFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MaxWithdrawFunction, BigInteger)(maxWithdrawFunction, blockParameter)
        
        End Function

        
        Public Function MaxWithdrawQueryAsync(ByVal [user] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim maxWithdrawFunction = New MaxWithdrawFunction()
                maxWithdrawFunction.User = [user]
            
            Return ContractHandler.QueryAsync(Of MaxWithdrawFunction, BigInteger)(maxWithdrawFunction, blockParameter)
        
        End Function


        Public Function MintRequestAsync(ByVal mintFunction As MintFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of MintFunction)(mintFunction)
        
        End Function

        Public Function MintRequestAndWaitForReceiptAsync(ByVal mintFunction As MintFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintFunction)(mintFunction, cancellationToken)
        
        End Function

        
        Public Function MintRequestAsync(ByVal [amount] As BigInteger, ByVal [to] As String) As Task(Of String)
        
            Dim mintFunction = New MintFunction()
                mintFunction.Amount = [amount]
                mintFunction.To = [to]
            
            Return ContractHandler.SendRequestAsync(Of MintFunction)(mintFunction)
        
        End Function

        
        Public Function MintRequestAndWaitForReceiptAsync(ByVal [amount] As BigInteger, ByVal [to] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim mintFunction = New MintFunction()
                mintFunction.Amount = [amount]
                mintFunction.To = [to]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintFunction)(mintFunction, cancellationToken)
        
        End Function
        Public Function NameQueryAsync(ByVal nameFunction As NameFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of NameFunction, String)(nameFunction, blockParameter)
        
        End Function

        
        Public Function NameQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of NameFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function NoncesQueryAsync(ByVal noncesFunction As NoncesFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of NoncesFunction, BigInteger)(noncesFunction, blockParameter)
        
        End Function

        
        Public Function NoncesQueryAsync(ByVal [returnValue1] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim noncesFunction = New NoncesFunction()
                noncesFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of NoncesFunction, BigInteger)(noncesFunction, blockParameter)
        
        End Function


        Public Function PermitRequestAsync(ByVal permitFunction As PermitFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of PermitFunction)(permitFunction)
        
        End Function

        Public Function PermitRequestAndWaitForReceiptAsync(ByVal permitFunction As PermitFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of PermitFunction)(permitFunction, cancellationToken)
        
        End Function

        
        Public Function PermitRequestAsync(ByVal [owner] As String, ByVal [spender] As String, ByVal [value] As BigInteger, ByVal [deadline] As BigInteger, ByVal [v] As Byte, ByVal [r] As Byte(), ByVal [s] As Byte()) As Task(Of String)
        
            Dim permitFunction = New PermitFunction()
                permitFunction.Owner = [owner]
                permitFunction.Spender = [spender]
                permitFunction.Value = [value]
                permitFunction.Deadline = [deadline]
                permitFunction.V = [v]
                permitFunction.R = [r]
                permitFunction.S = [s]
            
            Return ContractHandler.SendRequestAsync(Of PermitFunction)(permitFunction)
        
        End Function

        
        Public Function PermitRequestAndWaitForReceiptAsync(ByVal [owner] As String, ByVal [spender] As String, ByVal [value] As BigInteger, ByVal [deadline] As BigInteger, ByVal [v] As Byte, ByVal [r] As Byte(), ByVal [s] As Byte(), ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim permitFunction = New PermitFunction()
                permitFunction.Owner = [owner]
                permitFunction.Spender = [spender]
                permitFunction.Value = [value]
                permitFunction.Deadline = [deadline]
                permitFunction.V = [v]
                permitFunction.R = [r]
                permitFunction.S = [s]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of PermitFunction)(permitFunction, cancellationToken)
        
        End Function
        Public Function PreviewDepositQueryAsync(ByVal previewDepositFunction As PreviewDepositFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PreviewDepositFunction, BigInteger)(previewDepositFunction, blockParameter)
        
        End Function

        
        Public Function PreviewDepositQueryAsync(ByVal [amount] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim previewDepositFunction = New PreviewDepositFunction()
                previewDepositFunction.Amount = [amount]
            
            Return ContractHandler.QueryAsync(Of PreviewDepositFunction, BigInteger)(previewDepositFunction, blockParameter)
        
        End Function


        Public Function PreviewMintQueryAsync(ByVal previewMintFunction As PreviewMintFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PreviewMintFunction, BigInteger)(previewMintFunction, blockParameter)
        
        End Function

        
        Public Function PreviewMintQueryAsync(ByVal [amount] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim previewMintFunction = New PreviewMintFunction()
                previewMintFunction.Amount = [amount]
            
            Return ContractHandler.QueryAsync(Of PreviewMintFunction, BigInteger)(previewMintFunction, blockParameter)
        
        End Function


        Public Function PreviewRedeemQueryAsync(ByVal previewRedeemFunction As PreviewRedeemFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PreviewRedeemFunction, BigInteger)(previewRedeemFunction, blockParameter)
        
        End Function

        
        Public Function PreviewRedeemQueryAsync(ByVal [amount] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim previewRedeemFunction = New PreviewRedeemFunction()
                previewRedeemFunction.Amount = [amount]
            
            Return ContractHandler.QueryAsync(Of PreviewRedeemFunction, BigInteger)(previewRedeemFunction, blockParameter)
        
        End Function


        Public Function PreviewWithdrawQueryAsync(ByVal previewWithdrawFunction As PreviewWithdrawFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PreviewWithdrawFunction, BigInteger)(previewWithdrawFunction, blockParameter)
        
        End Function

        
        Public Function PreviewWithdrawQueryAsync(ByVal [amount] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim previewWithdrawFunction = New PreviewWithdrawFunction()
                previewWithdrawFunction.Amount = [amount]
            
            Return ContractHandler.QueryAsync(Of PreviewWithdrawFunction, BigInteger)(previewWithdrawFunction, blockParameter)
        
        End Function


        Public Function RedeemRequestAsync(ByVal redeemFunction As RedeemFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of RedeemFunction)(redeemFunction)
        
        End Function

        Public Function RedeemRequestAndWaitForReceiptAsync(ByVal redeemFunction As RedeemFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of RedeemFunction)(redeemFunction, cancellationToken)
        
        End Function

        
        Public Function RedeemRequestAsync(ByVal [amountStable] As BigInteger, ByVal [to] As String, ByVal [from] As String) As Task(Of String)
        
            Dim redeemFunction = New RedeemFunction()
                redeemFunction.AmountStable = [amountStable]
                redeemFunction.To = [to]
                redeemFunction.From = [from]
            
            Return ContractHandler.SendRequestAsync(Of RedeemFunction)(redeemFunction)
        
        End Function

        
        Public Function RedeemRequestAndWaitForReceiptAsync(ByVal [amountStable] As BigInteger, ByVal [to] As String, ByVal [from] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim redeemFunction = New RedeemFunction()
                redeemFunction.AmountStable = [amountStable]
                redeemFunction.To = [to]
                redeemFunction.From = [from]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of RedeemFunction)(redeemFunction, cancellationToken)
        
        End Function
        Public Function StableAssetsOfQueryAsync(ByVal stableAssetsOfFunction As StableAssetsOfFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of StableAssetsOfFunction, BigInteger)(stableAssetsOfFunction, blockParameter)
        
        End Function

        
        Public Function StableAssetsOfQueryAsync(ByVal [user] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim stableAssetsOfFunction = New StableAssetsOfFunction()
                stableAssetsOfFunction.User = [user]
            
            Return ContractHandler.QueryAsync(Of StableAssetsOfFunction, BigInteger)(stableAssetsOfFunction, blockParameter)
        
        End Function


        Public Function SymbolQueryAsync(ByVal symbolFunction As SymbolFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of SymbolFunction, String)(symbolFunction, blockParameter)
        
        End Function

        
        Public Function SymbolQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of SymbolFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function TotalAssetsQueryAsync(ByVal totalAssetsFunction As TotalAssetsFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of TotalAssetsFunction, BigInteger)(totalAssetsFunction, blockParameter)
        
        End Function

        
        Public Function TotalAssetsQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of TotalAssetsFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function TotalFloatQueryAsync(ByVal totalFloatFunction As TotalFloatFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of TotalFloatFunction, BigInteger)(totalFloatFunction, blockParameter)
        
        End Function

        
        Public Function TotalFloatQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of TotalFloatFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function TotalSupplyQueryAsync(ByVal totalSupplyFunction As TotalSupplyFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of TotalSupplyFunction, BigInteger)(totalSupplyFunction, blockParameter)
        
        End Function

        
        Public Function TotalSupplyQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of TotalSupplyFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function TransferRequestAsync(ByVal transferFunction As TransferFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of TransferFunction)(transferFunction)
        
        End Function

        Public Function TransferRequestAndWaitForReceiptAsync(ByVal transferFunction As TransferFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferFunction)(transferFunction, cancellationToken)
        
        End Function

        
        Public Function TransferRequestAsync(ByVal [to] As String, ByVal [amount] As BigInteger) As Task(Of String)
        
            Dim transferFunction = New TransferFunction()
                transferFunction.To = [to]
                transferFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAsync(Of TransferFunction)(transferFunction)
        
        End Function

        
        Public Function TransferRequestAndWaitForReceiptAsync(ByVal [to] As String, ByVal [amount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim transferFunction = New TransferFunction()
                transferFunction.To = [to]
                transferFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferFunction)(transferFunction, cancellationToken)
        
        End Function
        Public Function TransferFromRequestAsync(ByVal transferFromFunction As TransferFromFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of TransferFromFunction)(transferFromFunction)
        
        End Function

        Public Function TransferFromRequestAndWaitForReceiptAsync(ByVal transferFromFunction As TransferFromFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferFromFunction)(transferFromFunction, cancellationToken)
        
        End Function

        
        Public Function TransferFromRequestAsync(ByVal [from] As String, ByVal [to] As String, ByVal [amount] As BigInteger) As Task(Of String)
        
            Dim transferFromFunction = New TransferFromFunction()
                transferFromFunction.From = [from]
                transferFromFunction.To = [to]
                transferFromFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAsync(Of TransferFromFunction)(transferFromFunction)
        
        End Function

        
        Public Function TransferFromRequestAndWaitForReceiptAsync(ByVal [from] As String, ByVal [to] As String, ByVal [amount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim transferFromFunction = New TransferFromFunction()
                transferFromFunction.From = [from]
                transferFromFunction.To = [to]
                transferFromFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferFromFunction)(transferFromFunction, cancellationToken)
        
        End Function
        Public Function VolatileQueryAsync(ByVal @volatileFunction As VolatileFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of VolatileFunction, String)(@volatileFunction, blockParameter)
        
        End Function

        
        Public Function VolatileQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of VolatileFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function VolatileAssetsOfQueryAsync(ByVal volatileAssetsOfFunction As VolatileAssetsOfFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of VolatileAssetsOfFunction, BigInteger)(volatileAssetsOfFunction, blockParameter)
        
        End Function

        
        Public Function VolatileAssetsOfQueryAsync(ByVal [user] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim volatileAssetsOfFunction = New VolatileAssetsOfFunction()
                volatileAssetsOfFunction.User = [user]
            
            Return ContractHandler.QueryAsync(Of VolatileAssetsOfFunction, BigInteger)(volatileAssetsOfFunction, blockParameter)
        
        End Function


        Public Function WethQueryAsync(ByVal wethFunction As WethFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of WethFunction, String)(wethFunction, blockParameter)
        
        End Function

        
        Public Function WethQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of WethFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function WithdrawRequestAsync(ByVal withdrawFunction As WithdrawFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of WithdrawFunction)(withdrawFunction)
        
        End Function

        Public Function WithdrawRequestAndWaitForReceiptAsync(ByVal withdrawFunction As WithdrawFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of WithdrawFunction)(withdrawFunction, cancellationToken)
        
        End Function

        
        Public Function WithdrawRequestAsync(ByVal [amountStable] As BigInteger, ByVal [to] As String, ByVal [from] As String) As Task(Of String)
        
            Dim withdrawFunction = New WithdrawFunction()
                withdrawFunction.AmountStable = [amountStable]
                withdrawFunction.To = [to]
                withdrawFunction.From = [from]
            
            Return ContractHandler.SendRequestAsync(Of WithdrawFunction)(withdrawFunction)
        
        End Function

        
        Public Function WithdrawRequestAndWaitForReceiptAsync(ByVal [amountStable] As BigInteger, ByVal [to] As String, ByVal [from] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim withdrawFunction = New WithdrawFunction()
                withdrawFunction.AmountStable = [amountStable]
                withdrawFunction.To = [to]
                withdrawFunction.From = [from]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of WithdrawFunction)(withdrawFunction, cancellationToken)
        
        End Function
    
    End Class

End Namespace
