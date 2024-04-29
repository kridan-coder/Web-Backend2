using System;
using Backend2.Models;
using Backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IGreetingService greetingService;
        private readonly ICalculationService calculationService;

        public CalculatorController(IGreetingService greetingService, ICalculationService calculationService)
        {
            this.greetingService = greetingService;
            this.calculationService = calculationService;
        }
        
        private Int64 makeCalculation(string operand1, string operand2, string operation)
        {
            switch(operation)
            {
                case "+":
                    return calculationService.Addition(Convert.ToInt64(operand1), Convert.ToInt64(operand2));
                case "-":
                    return calculationService.Substraction(Convert.ToInt64(operand1), Convert.ToInt64(operand2));
                case "*":
                    return calculationService.Multiplication(Convert.ToInt64(operand1), Convert.ToInt64(operand2));
                case "/":
                    return calculationService.Division(Convert.ToInt64(operand1), Convert.ToInt64(operand2));
            }
            return -1;
        }


        public ActionResult Manual()
        {
            if (this.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                String operand1 = this.Request.Form["Form1"];
                String operand2 = this.Request.Form["Form2"];
                String op = this.Request.Form["OperatorSelector"];
                //String op = model.Option;

                bool errorWasFound = false;

                if (String.IsNullOrEmpty(operand1))
                {
                    this.ViewBag.ErrorForm1 = "First operand is required";
                    errorWasFound = true;
                }

                if (String.IsNullOrEmpty(operand2))
                {
                    this.ViewBag.ErrorForm2 = "Second operand is required";
                    errorWasFound = true;
                }
                else if (Convert.ToInt64(operand2) == 0 && op == "/")
                {
                    this.ViewBag.ErrorForm2 = "Деление на ноль запрещёно законом УК РФ 254 от 29.02.2021";
                    errorWasFound = true;
                }

                this.ViewBag.NumberForm1 = operand1;
                this.ViewBag.NumberForm2 = operand2;
                this.ViewBag.SelectorForm = op;

                if (errorWasFound)
                {
                    return this.View();
                }

                Int64 result = makeCalculation(operand1, operand2, op);
                var resultModel = new CalculatorModel
                {
                    Result = Convert.ToString(result)
                };

                return this.View(resultModel);
            }

            return this.View();
        }

        public ActionResult ManualWithSeparateHandlers()
        {
            return this.View();
        }

        [HttpPost, ActionName("ManualWithSeparateHandlers")]
        [ValidateAntiForgeryToken]
        public ActionResult ManualWithSeparateHandlersConfirm()
        {
            String operand1 = this.Request.Form["Form1"];
            String operand2 = this.Request.Form["Form2"];
            String op = this.Request.Form["OperatorSelector"];
            //String op = model.Option;

            bool errorWasFound = false;

            if (String.IsNullOrEmpty(operand1))
            {
                this.ViewBag.ErrorForm1 = "First operand is required";
                errorWasFound = true;
            }

            if (String.IsNullOrEmpty(operand2))
            {
                this.ViewBag.ErrorForm2 = "Second operand is required";
                errorWasFound = true;
            }
            else if (Convert.ToInt64(operand2) == 0 && op == "/")
            {
                this.ViewBag.ErrorForm2 = "Деление на ноль запрещёно законом УК РФ 254 от 29.02.2021";
                errorWasFound = true;
            }

            this.ViewBag.NumberForm1 = operand1;
            this.ViewBag.NumberForm2 = operand2;
            this.ViewBag.SelectorForm = op;

            if (errorWasFound)
            {
                return this.View();
            }

            Int64 result = makeCalculation(operand1, operand2, op);
            var resultModel = new CalculatorModel
            {
                Result = Convert.ToString(result)
            };

            return this.View(resultModel);
        }

        public ActionResult ModelBindingInParameters()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModelBindingInParameters(String Form1, String Form2, String OperatorSelector)
        {
            bool errorWasFound = false;

            if (String.IsNullOrEmpty(Form1))
            {
                this.ViewBag.ErrorForm1 = "First operand is required";
                errorWasFound = true;
            }

            if (String.IsNullOrEmpty(Form2))
            {
                this.ViewBag.ErrorForm2 = "Second operand is required";
                errorWasFound = true;
            }
            else if (Convert.ToInt64(Form2) == 0 && OperatorSelector == "/")
            {
                this.ViewBag.ErrorForm2 = "Деление на ноль запрещёно законом УК РФ 254 от 29.02.2021";
                errorWasFound = true;
            }

            this.ViewBag.NumberForm1 = Form1;
            this.ViewBag.NumberForm2 = Form2;
            this.ViewBag.SelectorForm = OperatorSelector;

            if (errorWasFound)
            {
                return this.View();
            }

            Int64 result = makeCalculation(Form1, Form2, OperatorSelector);
            var resultModel = new CalculatorModel
            {
                Result = Convert.ToString(result)
            };

            return this.View(resultModel);
        }

        public ActionResult ModelBindingInSeparateModel()
        {
            return this.View(new CalculatorModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModelBindingInSeparateModel(CalculatorModel model)
        {
            if (this.ModelState.IsValid)
            {

                if (Convert.ToInt64(model.Operand2) == 0 && model.Option == "/")
                {
                    this.ModelState.AddModelError("Operand2", "Деление на ноль запрещёно законом УК РФ 254 от 29.02.2021");
                    return this.View(model);
                }

                Int64 result = makeCalculation(model.Operand1, model.Operand2, model.Option);
                var resultModel = new CalculatorModel
                {
                    Result = Convert.ToString(result)
                };

                return this.View(resultModel);
            }

            return this.View(model);
        }
    }
}
