﻿using System;
using System.Collections.Generic;
using System.Linq;
using StructureComparer.Models;

namespace StructureComparer.Validators
{
    internal class ComplexTypeValidator : IBaseTypeValidator
    {
        private readonly IEnumerable<Func<Type, Type, StructureComparisonResult>> _validations;
        private readonly ITypeValidator _typeValidator;

        internal ComplexTypeValidator(ITypeValidator typeTypeValidator)
        {
            _typeValidator = typeTypeValidator;
        }

        public ComplexTypeValidator() : this(new TypeValidator())
        {
            _validations = new List<Func<Type, Type, StructureComparisonResult>>
            {
                ValidateComplexType
            };
        }

        public StructureComparisonResult Validate(Type baseType, Type toCompareType)
        {
            var comparisonResult = new StructureComparisonResult();

            foreach (var validation in _validations)
            {
                var validationResult = validation(baseType, toCompareType);

                if (!validationResult.AreEqual)
                    comparisonResult.AddError(validationResult.DifferencesString);
            }

            return comparisonResult;
        }

        private StructureComparisonResult ValidateComplexType(Type baseType, Type toCompareType)
        {
            var baseComplexType = baseType;
            var toCompareComplexType = toCompareType;

            if (BothTypesAreCollections(baseType, toCompareType))
            {
                baseComplexType = ExtractTypeCollection(baseType).FirstOrDefault();
                toCompareComplexType = ExtractTypeCollection(toCompareType).FirstOrDefault();
            }

            var comparisonResult = StructureComparer.Compare(baseComplexType, toCompareComplexType);
            return comparisonResult;
        }

        private bool BothTypesAreCollections(Type baseType, Type toCompareType)
        {
            return _typeValidator.IsEnumerableType(baseType) && 
                   _typeValidator.IsEnumerableType(toCompareType);
        }

        private static IEnumerable<Type> ExtractTypeCollection(Type type)
        {
            return type.GetGenericArguments();
        }
    }
}